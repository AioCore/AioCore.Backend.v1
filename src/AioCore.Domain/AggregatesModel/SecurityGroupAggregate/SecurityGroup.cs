using AioCore.Shared.Seedwork;
using Nest;
using System;

namespace AioCore.Domain.AggregatesModel.SecurityGroupAggregate
{
    public class SecurityGroup : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Keyword]
        public Guid ParentId { get; set; }

        public virtual SecurityGroup Parent { get; set; }

        [Keyword]
        public int IndexLeft { get; set; }

        [Keyword]
        public int IndexRight { get; set; }
    }
}