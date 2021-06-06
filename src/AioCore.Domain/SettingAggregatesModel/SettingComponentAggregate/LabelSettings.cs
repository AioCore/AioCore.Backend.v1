namespace AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate
{
    public class LabelSettings : IComponentSetting
    {
        public string InnerText { get; set; }

        public bool Hidden { get; set; }
    }
}
