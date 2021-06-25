using AioCore.Domain.Common;
using System.Collections.Generic;

namespace AioCore.Domain.CoreEntities
{
    public class SettingAction : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<SettingActionStep> SettingActionSteps { get; set; }
    }
}