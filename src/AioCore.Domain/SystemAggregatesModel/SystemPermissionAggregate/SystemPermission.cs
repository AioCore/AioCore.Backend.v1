using System;
using System.Collections.Generic;
using AioCore.Domain.SystemAggregatesModel.SystemPermissionSetAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SystemAggregatesModel.SystemPermissionAggregate
{
    public class SystemPermission : Entity, IAggregateRoot
    {
        public Guid RecordId { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<SystemPermissionSet> PermissionSets { get; set; }
    }
}