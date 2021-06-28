using AioCore.Application.ActionProcessors;
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
    public class DynamicCommand : IRequest<List<DynamicActionResponse>>
    {
        public Guid ComponentId { get; set; }
        public Dictionary<string, object> DynamicData { get; set; }

        internal class Handler : IRequestHandler<DynamicCommand, List<DynamicActionResponse>>
        {
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;
            private readonly ActionFactory _actionFactory;

            public Handler(IAioCoreUnitOfWork coreUnitOfWork, ActionFactory actionFactory)
            {
                _coreUnitOfWork = coreUnitOfWork;
                _actionFactory = actionFactory;
            }

            public async Task<Response<List<DynamicActionResponse>>> Handle(DynamicCommand request, CancellationToken cancellationToken)
            {
                var query = from t1 in _coreUnitOfWork.SettingComponents
                            join t2 in _coreUnitOfWork.SettingActions on t1.ParentId equals t2.Id
                            where t1.ComponentType == ComponentType.Action &&
                                t1.ParentType == ParentType.Form &&
                                t1.ParentId == request.ComponentId
                            select t2;
                var action = await query
                    .Include(t => t.ActionSteps)
                    .FirstOrDefaultAsync(cancellationToken);

                if (action is null) return null;

                var actionResponses = new List<DynamicActionResponse>();

                foreach (var step in action.ActionSteps.Where(t => t.Container == ActionContainer.Server))
                {
                    var processor = _actionFactory.GetProcessor(step.StepType);
                    var data = step.InitParamType == InitParamType.FormValue ? request.DynamicData : actionResponses[step.OutputStepOrder.Value].Data;
                    var processorTask = processor.ExecuteAsync(new ActionParamModel
                    {
                        StepId = step.Id,
                        TargetTypeId = step.TargetTypeId,
                        InitParamType = step.InitParamType,
                        TargetAttribute = step.TargetAttributeId,
                        Data = data
                    }, cancellationToken);
                    var result = step.IsFireAndForget ? null : await processorTask;
                    actionResponses.Add(new DynamicActionResponse
                    {
                        ComponentId = request.ComponentId,
                        ActionId = action.Id,
                        ActionStepId = step.Id,
                        Data = result
                    });
                }

                return actionResponses;
            }
        }
    }
}
