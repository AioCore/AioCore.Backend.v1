using System.Collections.Generic;

namespace AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate
{
    public class ActionSettings : IComponentSetting
    {
        public string ActionName { get; set; }

        // fontawesome: <i class="fad fa-acorn"></i>
        public string Icon { get; set; }

        public List<ActionStep> Steps { get; set; }

        public enum ActionStepType
        {
            SaveBase = 1,
            SaveRealtime
        }

        public enum ActionTriggerType
        {
            Client = 1,
            Server
        }

        public class ActionStep
        {
            public ActionStepType StepType { get; set; }

            public ActionTriggerType TriggerType { get; set; }
        }
    }
}
