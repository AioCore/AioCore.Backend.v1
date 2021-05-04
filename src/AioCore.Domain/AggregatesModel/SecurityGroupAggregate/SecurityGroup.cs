using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SecurityGroupAggregate
{
    public class SecurityGroup : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public Guid ParentId { get; set; }

        public virtual SecurityGroup Parent { get; set; }

        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }
    }
}