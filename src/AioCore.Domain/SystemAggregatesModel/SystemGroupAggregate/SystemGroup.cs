using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AioCore.Shared.Seedwork;
using Nest;
using Package.NestedSet;

namespace AioCore.Domain.SystemAggregatesModel.SystemGroupAggregate
{
    public class SystemGroup : INestedSet<SystemGroup, Guid, Guid?>, IAggregateRoot
    {
        public Guid Id { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [NotMapped]
        public virtual SystemGroup Parent { get; set; }

        public Guid? ParentId { get; set; }

        public int Level { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        public bool Moving { get; set; }

        [NotMapped]
        public virtual SystemGroup Root { get; set; }

        public Guid? RootId { get; set; }

        public virtual List<SystemGroup> Children { get; set; }

        public virtual List<SystemGroup> Descendants { get; set; }
    }
}