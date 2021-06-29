using AioCore.Application.ActionProcessors;
using AioCore.Application.Models;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.Models;
using AioCore.Shared;
using AioCore.Shared.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            public Handler(IAioCoreUnitOfWork coreUnitOfWork, ActionFactory actionFactory)
            {
                _coreUnitOfWork = coreUnitOfWork;
                _actionFactory = actionFactory;
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
                foreach (var step in action.ActionSteps.Where(t => t.Container == ActionContainer.Server))
                {
                    var processor = _actionFactory.GetProcessor(step.StepType);
                    var executeTask = processor.ExecuteAsync(new DynamicActionModel
                    {
                        Component = component,
                        ActionStep = step,
                        RequestData = request.DynamicData,
                        PreviousActionResults = actionResults.AsReadOnly()
                    }, cancellationToken);
                    var result = step.IsBackground ? null : await executeTask;
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
