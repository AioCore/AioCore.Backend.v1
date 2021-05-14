using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AioCore.Domain.AggregatesModel.SettingFeatureAggregate
{
    public class SettingFeature : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Icon { get; set; }

        [Column(TypeName = "xml")]
        public string XmlPage { get; set; }

        [Keyword]
        public Guid LayoutId { get; set; }

        public virtual SettingLayout Layout { get; set; }

        [Keyword]
        public int IndexLeft { get; set; }

        [Keyword]
        public int IndexRight { get; set; }
    }
}