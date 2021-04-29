using AioCore.Domain.AggregatesModel.DynamicEntityAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicFloatAggregate
{
    public class DynamicFloatValue : Entity, IAggregateRoot
    {
        public Guid AttributeId { get; set; }

        public Guid EntityId { get; set; }

        public float Value { get; set; }

        public virtual DynamicFloatAttribute Attribute { get; set; }

        public virtual DynamicEntity Entity { get; set; }
    }
}