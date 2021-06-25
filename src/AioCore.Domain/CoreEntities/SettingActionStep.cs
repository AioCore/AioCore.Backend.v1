using AioCore.Domain.Common;
using AioCore.Shared.Common;
using System;

namespace AioCore.Domain.CoreEntities
{
    public class SettingActionStep : Entity
    {
        public ActionDefinition Action { get; set; }
        public bool IsAsync { get; set; }
        public string Description { get; set; }
        public int Ordinal { get; set; }
        public Guid SettingActionId { get; set; }
        public virtual SettingAction SettingAction { get; set; }
    }
}
