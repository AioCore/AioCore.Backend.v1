using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicAggregate
{
    public class DynamicAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public Guid EntityTypeId { get; set; }
    }
}
