using AioCore.Domain.Common;

namespace AioCore.Domain.CoreEntities
{
    public class SettingFilter : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
