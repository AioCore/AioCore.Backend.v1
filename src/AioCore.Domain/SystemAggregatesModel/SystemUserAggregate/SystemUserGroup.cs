using System;
using AioCore.Domain.Common;
using AioCore.Domain.SystemAggregatesModel.SystemGroupAggregate;

namespace AioCore.Domain.SystemAggregatesModel.SystemUserAggregate
{
    public class SystemUserGroup : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

        public virtual SystemUser User { get; set; }

        public virtual SystemGroup Group { get; set; }
    }
}