using AioCore.Domain.AggregatesModel.DynamicEntityAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicStringAggregate
{
    public class DynamicStringValue : Entity, IAggregateRoot
    {
        public Guid AttributeId { get; set; }

        public Guid EntityId { get; set; }

        public string Value { get; set; }

        public virtual DynamicStringAttribute Attribute { get; set; }

        public virtual DynamicEntity Entity { get; set; }
    }
}