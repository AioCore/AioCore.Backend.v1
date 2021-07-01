using AioCore.Application.Models;
using AioCore.Domain.Models;
using AioCore.Mediator;
using AioCore.Shared.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;
using Plugin.ActionProcessor;

namespace AioCore.Application.DynamicCommand
{
    public class DynamicCommand : IRequest<List<DynamicActionResult>>
    {
        public Guid ComponentId { get; set; }
        public Dictionary<string, object> DynamicData { get; set; }

        internal class Handler : IRequestHandler<DynamicCommand, List<DynamicActionResult>>
        {
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;
            private readonly ActionFactory _actionFactory;
            private readonly Publisher _publisher;

            public Handler(
                  IAioCoreUnitOfWork coreUnitOfWork
                , ActionFactory actionFactory
                , Publisher publisher)
            {
                _coreUnitOfWork = coreUnitOfWork;
                _actionFactory = actionFactory;
                _publisher = publisher;
            }

            public async Task<Response<List<DynamicActionResult>>> Handle(DynamicCommand request, CancellationToken cancellationToken)
            {
                var component = await _coreUnitOfWork.SettingComponents
                    .FirstOrDefaultAsync(x => x.Id == request.ComponentId, cancellationToken);

                if (component is null) return null;

                var query = from t1 in _coreUnitOfWork.SettingComponents
                            join t2 in _coreUnitOfWork.SettingActions on t1.ParentId equals t2.Id
                            where t1.ComponentType == ComponentType.Action &&
                                t1.ParentType == ParentType.Form &&
                                t1.Id == request.ComponentId
                            select t2;
                var action = await query
                    .Include(t => t.ActionSteps)
                    .FirstOrDefaultAsync(cancellationToken);

                if (action is null) return null;

                var actionResults = new List<DynamicActionResult>();
                foreach (var step in action.ActionSteps.Where(t => t.Container == ActionContainer.Server).OrderBy(t => t.Ordinal))
                {
                    var processor = _actionFactory.GetProcessor(step.StepType);
                    Dictionary<string, object> result = null;
                    var actionModel = new DynamicActionModel
                    {
                        Component = component,
                        ActionStep = step,
                        RequestData = request.DynamicData,
                        PreviousActionResults = actionResults.AsReadOnly()
                    };
                    if (step.IsBackground)
                    {
                        //use mediator to send a notification and handle it in a background service
                        await _publisher.Publish(new DynamicPublisher { ActionModel = actionModel }, PublishStrategy.ParallelNoWait, cancellationToken);
                    }
                    else
                    {
                        result = await processor.ExecuteAsync(actionModel, cancellationToken);
                    }

                    actionResults.Add(new DynamicActionResult
                    {
                        ComponentId = component.ComponentId,
                        ActionId = action.Id,
                        ActionStepId = step.Id,
                        Data = result
                    });
                }

                return actionResults;
            }
        }
    }
}