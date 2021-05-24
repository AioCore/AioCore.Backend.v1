using AioCore.Domain.AggregatesModel.SystemTenantAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SystemApplicationAggregate
{
    public class SystemApplicationTenant : Entity, IAggregateRoot
    {
        public Guid ApplicationId { get; set; }

        public Guid TenantId { get; set; }

        public virtual SystemApplication Application { get; set; }

        public virtual SystemTenant Tenant { get; set; }
    }
}