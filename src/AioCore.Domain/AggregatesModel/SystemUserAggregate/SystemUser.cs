using AioCore.Domain.AggregatesModel.SystemTenantAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using Package.Extensions;
using System;
using System.Collections.Generic;

namespace AioCore.Domain.AggregatesModel.SystemUserAggregate
{
    public class SystemUser : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Keyword]
        public string Account { get; set; }

        [Keyword]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        [Keyword]
        public Guid TenantId { get; set; }

        public virtual SystemTenant Tenant { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }

        public virtual ICollection<SystemUserGroup> Groups { get; set; }

        public virtual ICollection<SystemUserPolicy> Policies { get; set; }

        public SystemUser()
        {
        }

        public SystemUser(
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

        public SystemUser(
            string name,
            string account,
            string email,
            string password)
        {
            Name = name;
            Account = account;
            Email = email;
            PasswordHash = password.CreateMd5();
        }

        public void Update(
            string name,
            string email)
        {
            Name = name;
            Email = email;
        }
    }
}