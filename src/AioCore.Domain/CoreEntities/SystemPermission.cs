using System;
using System.Collections.Generic;
using AioCore.Domain.Common;

namespace AioCore.Domain.CoreEntities
{
    public class SystemPermission : Entity, IAggregateRoot
    {
        public Guid RecordId { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<SystemPermissionSet> PermissionSets { get; set; }
    }
}