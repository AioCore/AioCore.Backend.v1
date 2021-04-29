using AioCore.Domain.AggregatesModel.SettingFeatureAggregate;
using AioCore.Shared.Seedwork;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AioCore.Domain.AggregatesModel.SettingDomAggregate
{
    public class SettingDom : Entity, IAggregateRoot
    {
        public string TagName { get; set; }

        [Column(TypeName = "xml")]
        public string Attributes { get; set; }

        [Column(TypeName = "xml")]
        public string AttributeValues { get; set; }

        public Guid ParentId { get; set; }

        public virtual SettingDom Parent { get; set; }

        public Guid FeatureId { get; set; }

        public SettingFeature Feature { get; set; }
    }
}