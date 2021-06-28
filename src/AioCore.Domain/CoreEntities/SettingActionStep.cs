using AioCore.Domain.Common;
using AioCore.Shared.Common;
using System;

namespace AioCore.Domain.CoreEntities
{
    public class SettingActionStep : Entity, IAggregateRoot
    {
        public StepType StepType { get; set; }
        public bool IsFireAndForget { get; set; }
        public ActionContainer Container { get; set; }
        public string Description { get; set; }
        public int Ordinal { get; set; }
        public Guid ActionId { get; set; }
        public Guid TargetTypeId { get; set; }
        public InitParamType InitParamType { get; set; }
        public int? OutputStepOrder { get; set; }
        public Guid? TargetAttributeId { get; set; }
        public string Conditions { get; set; }
        public string ReturnType { get; set; }
        public virtual SettingAction Action { get; set; }
        public virtual SettingEntityType TargetType { get; set; }
    }   
}
