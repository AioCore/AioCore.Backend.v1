using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System;

namespace AioCore.Domain.AggregatesModel.SettingEntityAggregate
{
    public class SettingEntity : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public Guid TenantId { get; set; }

        public virtual SettingTenant Tenant { get; set; }
    }
}