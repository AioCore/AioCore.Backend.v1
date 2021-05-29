using AioCore.Domain.AggregatesModel.SystemUserAggregate;
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

        public string FilePath { get; set; }

        public Guid? ParentId { get; set; }

        public long ViewCount { get; set; }

        public DateTimeOffset Created { get; set; }

        public Guid? AuthorId { get; set; }

        public long Size { get; set; }

        public SizeType SizeType { get; set; }

        public string ContentType { get; set; }
    }
}