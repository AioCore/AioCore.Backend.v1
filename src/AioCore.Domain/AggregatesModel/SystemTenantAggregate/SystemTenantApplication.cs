using AioCore.Domain.AggregatesModel.SystemApplicationAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SystemTenantAggregate
{
    public class SystemTenantApplication : Entity, IAggregateRoot
    {
        public Guid TenantId { get; set; }

        public Guid ApplicationId { get; set; }

        public virtual SystemTenant Tenant { get; set; }

        public virtual SystemApplication Application { get; set; }
    }
}