using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SettingEntityAggregate
{
    public class SettingEntity : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TenantId { get; set; }

        public virtual SettingTenant Tenant { get; set; }
    }
}