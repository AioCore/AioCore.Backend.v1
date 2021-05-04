using System;
using AioCore.Domain.AggregatesModel.SecurityPermissionAggregate;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SecurityPermissionSetAggregate
{
    public class SecurityPermissionSet : Entity, IAggregateRoot
    {
        public Guid PermissionId { get; set; }

        public PermissionPolicy Policy { get; set; }

        public virtual SecurityPermission Permission { get; set; }
    }
}