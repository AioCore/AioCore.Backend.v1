namespace AioCore.Domain.Models
{
    public class LabelSettings : IComponentSetting
    {
        public string InnerText { get; set; }

        public bool Hidden { get; set; }
    }
}
