using System;
using AioCore.Domain.Common;
using AioCore.Shared.Common;

namespace AioCore.Domain.DynamicEntities
{
    public class DynamicAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public DataType DataType { get; set; }

        public Guid EntityTypeId { get; set; }
    }
}