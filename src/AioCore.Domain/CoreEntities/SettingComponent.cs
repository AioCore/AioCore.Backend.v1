using AioCore.Domain.Common;
using AioCore.Domain.Models;
using Newtonsoft.Json;
using System;

namespace AioCore.Domain.CoreEntities
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