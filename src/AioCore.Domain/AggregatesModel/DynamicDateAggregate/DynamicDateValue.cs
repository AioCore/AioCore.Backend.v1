using AioCore.Domain.AggregatesModel.DynamicEntityAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicDateAggregate
{
    public class DynamicDateValue : Entity, IAggregateRoot
    {
        public Guid AttributeId { get; set; }

        public Guid EntityId { get; set; }

        public DateTimeOffset Value { get; set; }

        public virtual DynamicDateAttribute Attribute { get; set; }

        public virtual DynamicEntity Entity { get; set; }
    }
}