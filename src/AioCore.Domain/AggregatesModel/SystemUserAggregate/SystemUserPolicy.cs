using AioCore.Domain.AggregatesModel.SystemPolicyAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SystemUserAggregate
{
    public class SystemUserPolicy : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }

        public Guid PolicyId { get; set; }

        public virtual SystemUser User { get; set; }

        public virtual SystemPolicy Policy { get; set; }
    }
}