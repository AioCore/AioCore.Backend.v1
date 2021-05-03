using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SettingComponentAggregate
{
    public class SettingComponent : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public Guid ParentId { get; set; }

        public ParentType ParentType { get; set; }

        public Guid ComponentId { get; set; }

        public ComponentType ComponentType { get; set; }
    }
}