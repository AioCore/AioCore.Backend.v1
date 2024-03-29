﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using AioCore.Domain.Common;

namespace AioCore.Domain.CoreEntities
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

        public Guid ComponentId { get; set; }

        public virtual SettingComponent Component { get; set; }

        public Guid FeatureId { get; set; }

        public virtual SettingFeature Feature { get; set; }
    }
}