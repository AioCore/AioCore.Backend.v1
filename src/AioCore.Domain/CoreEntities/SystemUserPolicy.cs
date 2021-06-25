using System;
using AioCore.Domain.Common;

namespace AioCore.Domain.CoreEntities
{
    public class SystemUserPolicy : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }

        public Guid PolicyId { get; set; }

        public virtual SystemUser User { get; set; }

        public virtual SystemPolicy Policy { get; set; }
    }
}