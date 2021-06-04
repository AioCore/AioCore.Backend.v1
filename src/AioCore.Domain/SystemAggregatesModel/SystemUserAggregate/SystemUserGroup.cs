using System;
using AioCore.Domain.SystemAggregatesModel.SystemGroupAggregate;
using AioCore.Shared.Seedwork;

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