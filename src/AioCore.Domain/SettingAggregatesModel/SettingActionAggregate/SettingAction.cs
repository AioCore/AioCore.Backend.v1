using AioCore.Shared.Seedwork;
using Nest;

namespace AioCore.Domain.SettingAggregatesModel.SettingActionAggregate
{
    public class SettingAction : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        public string Icon { get; set; }
    }
}