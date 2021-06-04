using System;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using AioCore.Shared.Seedwork;
using Nest;

namespace AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate
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