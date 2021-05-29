using AioCore.Domain.AggregatesModel.SystemGroupAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SystemUserAggregate
{
    public class SystemUserGroup : Entity, IAggregateRoot
    {
        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

        public virtual SystemUser User { get; set; }

        public virtual SystemGroup Group { get; set; }
    }
}