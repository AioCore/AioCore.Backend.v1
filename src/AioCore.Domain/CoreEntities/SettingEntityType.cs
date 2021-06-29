using System;
using AioCore.Domain.Common;
using Nest;

namespace AioCore.Domain.CoreEntities
{
    public class SettingEntityType : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public Guid TenantId { get; set; }

        public virtual SystemTenant Tenant { get; set; }
    }
}