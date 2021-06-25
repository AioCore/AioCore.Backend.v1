using System;
using AioCore.Domain.Common;
using AioCore.Domain.Models;

namespace AioCore.Domain.CoreEntities
{
    public class SystemPermissionSet : Entity, IAggregateRoot
    {
        public Guid PermissionId { get; set; }

        public PermissionPolicy Policy { get; set; }

        public virtual SystemPermission Permission { get; set; }
    }
}