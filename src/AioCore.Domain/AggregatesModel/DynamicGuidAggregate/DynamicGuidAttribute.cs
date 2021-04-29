using System.Collections.Generic;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.DynamicGuidAggregate
{
    public class DynamicGuidAttribute : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public virtual ICollection<DynamicGuidValue> DynamicGuidValues { get; set; }
    }
}