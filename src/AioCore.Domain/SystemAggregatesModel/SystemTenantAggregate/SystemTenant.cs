using System;
using System.Collections.Generic;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using AioCore.Shared.Seedwork;
using Nest;

namespace AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate
{
    public class SystemTenant : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public Guid? FaviconId { get; set; }

        [Keyword]
        public Guid? LogoId { get; set; }

        public string Server { get; set; }

        public string User { get; set; }

        public string Database { get; set; }

        public string Password { get; set; }

        public string Schema { get; set; }

        public string DatabaseType { get; set; }

        public virtual ICollection<SystemUser> Users { get; set; }

        public virtual ICollection<SystemTenantApplication> TenantApplications { get; set; }

        public SystemTenant()
        {
        }

        public SystemTenant(
            string name,
            string description)
        {
            Name = name;
            Description = description;
        }
    }
}