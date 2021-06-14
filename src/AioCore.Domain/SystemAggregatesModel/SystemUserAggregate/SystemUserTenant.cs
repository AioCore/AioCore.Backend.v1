using AioCore.Domain.Common;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using System;

namespace AioCore.Domain.SystemAggregatesModel.SystemUserAggregate
{
    public class SystemUserTenant : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }

        public virtual SystemUser User { get; set; }
        public virtual SystemTenant Tenant { get; set; }
    }
}
