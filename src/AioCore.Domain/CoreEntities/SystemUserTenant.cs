using AioCore.Domain.Common;
using System;

namespace AioCore.Domain.CoreEntities
{
    public class SystemUserTenant : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }

        public virtual SystemUser User { get; set; }
        public virtual SystemTenant Tenant { get; set; }
    }
}
