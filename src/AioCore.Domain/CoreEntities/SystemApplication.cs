using System;
using System.Collections.Generic;
using AioCore.Domain.Common;
using Nest;

namespace AioCore.Domain.CoreEntities
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