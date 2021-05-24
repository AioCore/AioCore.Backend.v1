using System;
using AioCore.Shared.Seedwork;
using Nest;

namespace AioCore.Domain.AggregatesModel.SystemGroupAggregate
{
    public class SystemGroup : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Keyword]
        public Guid ParentId { get; set; }

        public virtual SystemGroup Parent { get; set; }

        [Keyword]
        public int IndexLeft { get; set; }

        [Keyword]
        public int IndexRight { get; set; }
    }
}