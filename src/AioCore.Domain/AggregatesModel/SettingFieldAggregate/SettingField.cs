using AioCore.Shared.Seedwork;
using Nest;

namespace AioCore.Domain.AggregatesModel.SettingFieldAggregate
{
    public class SettingField : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public FieldType FieldType { get; set; }
    }
}