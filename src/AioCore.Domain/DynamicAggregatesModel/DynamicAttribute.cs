using System;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.DynamicAggregatesModel
{
    public class DynamicAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public Guid EntityTypeId { get; set; }
    }
}