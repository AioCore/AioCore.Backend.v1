using System;
using System.Collections.Generic;
using AioCore.Domain.AggregatesModel.SystemPermissionSetAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SystemPermissionAggregate
{
    public class SystemPermission : Entity, IAggregateRoot
    {
        public Guid RecordId { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<SystemPermissionSet> PermissionSets { get; set; }
    }
}