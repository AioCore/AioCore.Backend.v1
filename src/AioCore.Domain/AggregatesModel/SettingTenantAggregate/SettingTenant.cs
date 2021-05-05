using AioCore.Shared.Seedwork;
using Nest;
using System;

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