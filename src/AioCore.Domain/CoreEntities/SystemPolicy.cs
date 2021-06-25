using AioCore.Domain.Common;

namespace AioCore.Domain.CoreEntities
{
    public class SystemPolicy : Entity, IAggregateRoot
    {
        public string Controller { get; set; }

        public string Action { get; set; }
    }
}