using System.ComponentModel.DataAnnotations;

namespace AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate
{
    public class FieldSettings : IComponentSetting
    {
        public string Caption { get; set; }

        public string PlaceHolder { get; set; }

        public DataType DataType { get; set; }

        public bool Hidden { get; set; }
    }
}
