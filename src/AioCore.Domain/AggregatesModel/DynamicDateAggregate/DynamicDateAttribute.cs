using System.Collections.Generic;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.DynamicDateAggregate
{
    public class DynamicDateAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<DynamicDateValue> DynamicDateValues { get; set; }
    }
}