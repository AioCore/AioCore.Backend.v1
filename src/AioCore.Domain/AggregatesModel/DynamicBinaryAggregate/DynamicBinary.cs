using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicBinaryAggregate
{
    public class DynamicBinary : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string SourceName { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string DestinationName { get; set; }

        [Text]
        public string FilePath { get; set; }

        [Keyword]
        public Guid? ParentId { get; set; }

        public long ViewCount { get; set; }

        [Date]
        public DateTimeOffset Created { get; set; }

        [Keyword]
        public Guid? AuthorId { get; set; }

        [Nested]
        public virtual SecurityUser Author { get; set; }

        [Number]
        public long Size { get; set; }

        [Keyword] 
        public SizeType SizeType { get; set; }

        [Text]
        public string ContentType { get; set; }
    }
}