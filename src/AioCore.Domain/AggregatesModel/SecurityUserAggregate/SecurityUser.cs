using System;
using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SecurityUserAggregate
{
    public class SecurityUser : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Guid TenantId { get; set; }

        public virtual SettingTenant Tenant { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}