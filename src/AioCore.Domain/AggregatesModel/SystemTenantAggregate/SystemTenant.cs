using AioCore.Domain.AggregatesModel.SystemUserAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System;
using System.Collections.Generic;

namespace AioCore.Domain.AggregatesModel.SystemTenantAggregate
{
    public class SystemTenant : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public Guid FaviconId { get; set; }

        [Keyword]
        public Guid LogoId { get; set; }

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