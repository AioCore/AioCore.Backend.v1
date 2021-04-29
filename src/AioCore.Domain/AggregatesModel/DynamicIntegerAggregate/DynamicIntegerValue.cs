using AioCore.Domain.AggregatesModel.DynamicEntityAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicIntegerAggregate
{
    public class DynamicIntegerValue : Entity, IAggregateRoot
    {
        public Guid AttributeId { get; set; }

        public Guid EntityId { get; set; }

        public int Value { get; set; }

        public virtual DynamicIntegerAttribute Attribute { get; set; }

        public virtual DynamicEntity Entity { get; set; }
    }
}