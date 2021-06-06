using AioCore.Domain.SettingAggregatesModel.SettingFieldAggregate;
using AioCore.Shared.Seedwork;
using System;
using System.Collections.Generic;

namespace AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate
{
    public class SettingComponent : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Caption { get; set; }

        public Guid ParentId { get; set; }

        public ParentType ParentType { get; set; }

        public Guid ComponentId { get; set; }

        /// <summary>
        /// ComponentType = Field => Settings = FieldSettings
        /// </summary>
        public ComponentType ComponentType { get; set; }

        public string ClassName { get; set; }

        public string Settings { get; set; }

        public class FieldSettings
        {
            public string Caption { get; set; }

            public string PlaceHolder { get; set; }

            public DataType DataType { get; set; }

            public bool Hidden { get; set; }
        }

        public class ActionSettings
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
}