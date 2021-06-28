using AioCore.Domain.CoreEntities;
using AioCore.Shared.Common;
using Package.AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.ActionProcessors
{
    public interface IActionProcessor
    {
        StepType StepType { get; }

        Task<Dictionary<string, object>> ExecuteAsync(ActionParamModel actionParam, CancellationToken cancellationToken);
    }

    public class ActionParamModel : IMapFrom<SettingActionStep>
    {
        public Guid StepId { get; set; }
        public Guid TargetTypeId { get; set; }
        public Guid? TargetAttribute { get; set; }
        public InitParamType InitParamType { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
