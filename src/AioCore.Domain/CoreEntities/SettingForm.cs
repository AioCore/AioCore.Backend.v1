﻿using AioCore.Domain.Common;
using Nest;

namespace AioCore.Domain.CoreEntities
{
    public class SettingForm : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }
    }
}