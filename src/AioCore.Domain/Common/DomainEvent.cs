using System;

namespace AioCore.Domain.Common
{
    public abstract class DomainEvent
    {
        public bool IsPublished { get; set; }

        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;

        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
    }
}
