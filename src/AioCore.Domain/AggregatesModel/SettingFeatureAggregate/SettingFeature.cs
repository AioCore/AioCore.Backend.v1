using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using Package.NestedSet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AioCore.Domain.AggregatesModel.SettingFeatureAggregate
{
    public class SettingFeature : INestedSet<SettingFeature, Guid, Guid?>, IAggregateRoot
    {
        public Guid Id { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Icon { get; set; }

        [Column(TypeName = "xml")]
        public string XmlPage { get; set; }

        [Keyword]
        public Guid LayoutId { get; set; }

        public virtual SettingLayout Layout { get; set; }

        public virtual SettingFeature Parent { get; set; }

        public Guid? ParentId { get; set; }

        public int Level { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        public bool Moving { get; set; }

        public virtual SettingFeature Root { get; set; }

        public Guid? RootId { get; set; }

        public virtual List<SettingFeature> Children { get; set; }

        public virtual List<SettingFeature> Descendants { get; set; }
    }
}