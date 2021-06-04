using System;
using AioCore.Domain.SystemAggregatesModel.SystemPermissionAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SystemAggregatesModel.SystemPermissionSetAggregate
{
    public class SystemPermissionSet : Entity, IAggregateRoot
    {
        public Guid PermissionId { get; set; }

        public PermissionPolicy Policy { get; set; }

        public virtual SystemPermission Permission { get; set; }
    }
}