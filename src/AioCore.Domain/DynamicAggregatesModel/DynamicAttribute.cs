using System;
using AioCore.Domain.Common;

namespace AioCore.Domain.DynamicAggregatesModel
{
    public class DynamicAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string DataType { get; set; }

        public Guid EntityTypeId { get; set; }
    }
}