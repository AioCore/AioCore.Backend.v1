﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AioCore.Domain.Common;
using Nest;
using Package.NestedSet;

namespace AioCore.Domain.CoreEntities
{
    public class SettingFeature : Entity, INestedSet<SettingFeature, Guid, Guid?>, IAggregateRoot
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

        [NotMapped]
        public virtual SettingFeature Parent { get; set; }

        public Guid? ParentId { get; set; }

        public int Level { get; set; }

        public int Left { get; set; }

        public int Right { get; set; }

        public bool Moving { get; set; }

        [NotMapped]
        public virtual SettingFeature Root { get; set; }

        public Guid? RootId { get; set; }

        public virtual List<SettingFeature> Children { get; set; }

        public virtual List<SettingFeature> Descendants { get; set; }
    }
}