using AioCore.Domain.Common;
using Newtonsoft.Json;
using System;

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

        public T GetComponentSettings<T>() where T : IComponentSetting
        {
            if (Settings is null) return default;
            return JsonConvert.DeserializeObject<T>(Settings);
        }        
    }
}