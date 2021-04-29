using System.Collections.Generic;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.DynamicStringAggregate
{
    public class DynamicStringAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<DynamicStringValue> DynamicStringValues { get; set; }
    }
}