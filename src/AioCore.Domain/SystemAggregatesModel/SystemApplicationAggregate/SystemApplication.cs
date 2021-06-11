using System;
using System.Collections.Generic;
using AioCore.Domain.Common;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using Nest;

namespace AioCore.Domain.SystemAggregatesModel.SystemApplicationAggregate
{
    public class SystemApplication : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public Guid LogoId { get; set; }

        public virtual ICollection<SystemTenantApplication> TenantApplications { get; set; }
    }
}