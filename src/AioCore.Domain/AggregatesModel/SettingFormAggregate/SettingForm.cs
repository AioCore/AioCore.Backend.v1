using AioCore.Shared.Seedwork;
using Nest;

namespace AioCore.Domain.AggregatesModel.SettingFormAggregate
{
    public class SettingForm : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }
    }
}