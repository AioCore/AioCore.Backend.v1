using System;
using AioCore.Domain.Common;

namespace AioCore.Domain.CoreEntities
{
    public class SystemTenantApplication : Entity, IAggregateRoot
    {
        public Guid TenantId { get; set; }

        public Guid ApplicationId { get; set; }

        public virtual SystemTenant Tenant { get; set; }

        public virtual SystemApplication Application { get; set; }
    }
}