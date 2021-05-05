using AioCore.Shared.Seedwork;
using Nest;

namespace AioCore.Domain.AggregatesModel.SettingViewAggregate
{
    public class SettingView : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }
    }
}