using AioCore.Shared.Common;
using Package.Elasticsearch;
using System;
using System.Collections.Generic;

namespace AioCore.Domain.Models
{
    public class FilterSettings : IComponentSetting
    {
        public Guid EntityTypeId { get; set; }

        public List<FilterParameter> Parameters { get; set; } = new List<FilterParameter>();
    }

    public class FilterParameter
    {
        public Guid AttributeId { get; set; }
        public Operator Operator { get; set; }
        public Function Function { get; set; }
    }
}
