using System.Collections.Generic;
using AioCore.Domain.Common;
using AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingDomAggregate;
using Nest;

namespace AioCore.Domain.SettingAggregatesModel.SettingLayoutAggregate
{
    public class SettingLayout : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        public virtual ICollection<SettingComponent> Components { get; set; }

        public virtual ICollection<SettingDom> Doms { get; set; }

        public SettingLayout()
        {
        }

        public SettingLayout(
            string name,
            string description)
        {
            Name = name;
            Description = description;
        }
    }
}