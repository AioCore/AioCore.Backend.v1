using AioCore.Shared.Seedwork;
using Nest;
using System;
using AioCore.Domain.AggregatesModel.SystemTenantAggregate;

namespace AioCore.Domain.AggregatesModel.SettingEntityAggregate
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