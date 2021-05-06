using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System;
using System.Collections.Generic;

namespace AioCore.Domain.AggregatesModel.SettingTenantAggregate
{
    public class SettingTenant : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public Guid FaviconId { get; set; }

        [Keyword]
        public Guid LogoId { get; set; }

        public virtual ICollection<SecurityUser> Users { get; set; }

        public SettingTenant()
        {
        }

        public SettingTenant(
            string name,
            string description)
        {
            Name = name;
            Description = description;
        }
    }
}