using AioCore.Shared.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AioCore.Domain.AggregatesModel.DynamicAggregate
{
    public class DynamicEntity : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        [Column(TypeName = "xml")]
        public string Data { get; set; }

        public Guid EntityTypeId { get; set; }

        public Guid TenantId { get; set; }

        public DateTimeOffset Created { get; set; }

        public virtual ICollection<DynamicDateValue> DynamicDateValues { get; set; }

        public virtual ICollection<DynamicFloatValue> DynamicFloatValues { get; set; }

        public virtual ICollection<DynamicGuidValue> DynamicGuidValues { get; set; }

        public virtual ICollection<DynamicIntegerValue> DynamicIntegerValues { get; set; }

        public virtual ICollection<DynamicStringValue> DynamicStringValues { get; set; }
    }
}