using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SystemAggregatesModel.SystemPolicyAggregate
{
    public class SystemPolicy : Entity, IAggregateRoot
    {
        public string Controller { get; set; }

        public string Action { get; set; }
    }
}