using AioCore.Domain.Common;
using Nest;

namespace AioCore.Domain.SettingAggregatesModel.SettingFieldAggregate
{
    public class SettingField : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        [Keyword]
        public DataType FieldType { get; set; }

        public bool AllowSearch { get; set; }

        public bool AllowSequence { get; set; }
    }
}