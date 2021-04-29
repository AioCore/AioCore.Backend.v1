using AioCore.Domain.AggregatesModel.DynamicEntityAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicGuidAggregate
{
    public class DynamicGuidValue : Entity, IAggregateRoot
    {
        public Guid AttributeId { get; set; }

        public Guid EntityId { get; set; }

        public Guid Value { get; set; }

        public virtual DynamicGuidAttribute Attribute { get; set; }

        public virtual DynamicEntity Entity { get; set; }
    }
}