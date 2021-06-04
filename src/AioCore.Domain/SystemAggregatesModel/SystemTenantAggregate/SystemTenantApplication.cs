using System;
using AioCore.Domain.SystemAggregatesModel.SystemApplicationAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate
{
    public class SystemTenantApplication : Entity, IAggregateRoot
    {
        public Guid TenantId { get; set; }

        public Guid ApplicationId { get; set; }

        public virtual SystemTenant Tenant { get; set; }

        public virtual SystemApplication Application { get; set; }
    }
}