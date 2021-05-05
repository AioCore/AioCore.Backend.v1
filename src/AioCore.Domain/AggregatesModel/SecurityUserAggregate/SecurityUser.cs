using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System;

namespace AioCore.Domain.AggregatesModel.SecurityUserAggregate
{
    public class SecurityUser : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Keyword]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        [Keyword]
        public Guid TenantId { get; set; }

        public virtual SettingTenant Tenant { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}