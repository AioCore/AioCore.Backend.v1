using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SettingTenantAggregate
{
    public class SettingTenant : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid FaviconId { get; set; }

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