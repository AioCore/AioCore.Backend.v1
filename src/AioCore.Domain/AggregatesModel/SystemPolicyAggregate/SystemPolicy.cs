using System;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SystemPolicyAggregate
{
    public class SystemPolicy : Entity, IAggregateRoot
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }
    }
}