﻿namespace AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate
{
    public class FormSettings : IComponentSetting
    {
        public string Method { get; set; }

        public string Enctype { get; set; }

        public string InnerText { get; set; }
    }
}
