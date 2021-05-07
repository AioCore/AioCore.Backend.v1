using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.DynamicBinaryAggregate
{
    public class DynamicBinary : Entity, IAggregateRoot
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public Guid ParentId { get; set; }

        public long ViewCount { get; set; }

        public DateTimeOffset Created { get; set; }

        public Guid? AuthorId { get; set; }

        public virtual SecurityUser Author { get; set; }

        public long Size { get; set; }

        public string ContentType { get; set; }
    }
}