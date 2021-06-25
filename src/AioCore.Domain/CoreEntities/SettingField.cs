using AioCore.Domain.Common;
using AioCore.Shared.Common;
using Nest;

namespace AioCore.Domain.CoreEntities
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