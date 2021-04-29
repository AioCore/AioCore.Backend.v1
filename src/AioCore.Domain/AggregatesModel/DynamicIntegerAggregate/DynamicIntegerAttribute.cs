using System.Collections.Generic;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.DynamicIntegerAggregate
{
    public class DynamicIntegerAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<DynamicIntegerValue> DynamicIntegerValues { get; set; }
    }
}