using AioCore.Domain.AggregatesModel.SystemTenantAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System;
using System.Collections.Generic;

namespace AioCore.Domain.AggregatesModel.SystemApplicationAggregate
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