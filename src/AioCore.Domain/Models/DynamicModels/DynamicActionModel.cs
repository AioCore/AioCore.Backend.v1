using AioCore.Domain.CoreEntities;
using System.Collections.Generic;


namespace AioCore.Application.Models
{
    public class DynamicActionModel
    {
        public SettingAction Action { get; set; }
        public SettingActionStep ActionStep { get; set; }
        public Dictionary<string, object> RequestData { get; set; }
        public IReadOnlyCollection<DynamicActionResult> PreviousActionResults { get; set; }
    }
}
