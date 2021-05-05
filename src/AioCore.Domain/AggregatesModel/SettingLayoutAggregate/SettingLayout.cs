using AioCore.Domain.AggregatesModel.SettingComponentAggregate;
using AioCore.Domain.AggregatesModel.SettingDomAggregate;
using AioCore.Shared.Seedwork;
using Nest;
using System.Collections.Generic;

namespace AioCore.Domain.AggregatesModel.SettingLayoutAggregate
{
    public class SettingLayout : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Description { get; set; }

        public virtual ICollection<SettingComponent> Components { get; set; }

        public virtual ICollection<SettingDom> Doms { get; set; }
    }
}