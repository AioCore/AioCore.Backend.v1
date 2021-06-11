using System;
using AioCore.Domain.Common;

namespace AioCore.Domain.DynamicAggregatesModel
{
    public abstract class DynamicValue<T> : Entity, IAggregateRoot
    {
        public Guid AttributeId { get; set; }

        public Guid EntityId { get; set; }

        public Guid EntityTypeId { get; set; }

        public T Value { get; set; }

        public virtual DynamicAttribute Attribute { get; set; }

        public virtual DynamicEntity Entity { get; set; }
    }
}
