using System.Collections.Generic;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.DynamicFloatAggregate
{
    public class DynamicFloatAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<DynamicFloatValue> DynamicFloatValues { get; set; }
    }
}