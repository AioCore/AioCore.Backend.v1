using System;
using AioCore.Domain.AggregatesModel.SystemPermissionAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SystemPermissionSetAggregate
{
    public class SystemPermissionSet : Entity, IAggregateRoot
    {
        public Guid PermissionId { get; set; }

        public PermissionPolicy Policy { get; set; }

        public virtual SystemPermission Permission { get; set; }
    }
}