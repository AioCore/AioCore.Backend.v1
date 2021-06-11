using System;
using AioCore.Domain.Common;
using AioCore.Domain.SystemAggregatesModel.SystemPermissionAggregate;

namespace AioCore.Domain.SystemAggregatesModel.SystemPermissionSetAggregate
{
    public class SystemPermissionSet : Entity, IAggregateRoot
    {
        public Guid PermissionId { get; set; }

        public PermissionPolicy Policy { get; set; }

        public virtual SystemPermission Permission { get; set; }
    }
}