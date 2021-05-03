using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SettingViewAggregate
{
    public class SettingView : Entity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}