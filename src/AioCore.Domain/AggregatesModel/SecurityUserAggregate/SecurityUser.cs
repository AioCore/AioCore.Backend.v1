using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using Package.Extensions;
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

        public SecurityUser()
        {
        }

        public SecurityUser(
            string name,
            string email,
            Guid tenantId,
            string password)
        {
            Name = name;
            Email = email;
            TenantId = tenantId;
            PasswordHash = password.CreateMd5();
        }

        public SecurityUser(
            string name,
            string email,
            string password)
        {
            Name = name;
            Email = email;
            PasswordHash = password.CreateMd5();
        }
    }
}