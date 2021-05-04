using System;
using System.Collections.Generic;
using AioCore.Domain.AggregatesModel.SecurityPermissionSetAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SecurityPermissionAggregate
{
    public class SecurityPermission : Entity, IAggregateRoot
    {
        public Guid RecordId { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<SecurityPermissionSet> PermissionSets { get; set; }
    }
}