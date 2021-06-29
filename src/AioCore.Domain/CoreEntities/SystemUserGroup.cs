using System;
using AioCore.Domain.Common;

namespace AioCore.Domain.CoreEntities
{
    public class SystemUserGroup : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

        public virtual SystemUser User { get; set; }

        public virtual SystemGroup Group { get; set; }
    }
}