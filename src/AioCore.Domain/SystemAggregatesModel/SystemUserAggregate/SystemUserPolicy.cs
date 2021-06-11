using System;
using AioCore.Domain.Common;
using AioCore.Domain.SystemAggregatesModel.SystemPolicyAggregate;

namespace AioCore.Domain.SystemAggregatesModel.SystemUserAggregate
{
    public class SystemUserPolicy : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }

        public Guid PolicyId { get; set; }

        public virtual SystemUser User { get; set; }

        public virtual SystemPolicy Policy { get; set; }
    }
}