using System;
using System.ComponentModel.DataAnnotations;

namespace AioCore.Web.ViewModels
{
    public class SettingTenantViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Domain { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid AvatarPhoto { get; set; }

        public Guid CoverPhoto { get; set; }

        [Required]
        public string DatabaseServer { get; set; }

        [Required]
        public string DatabaseUser { get; set; }

        [Required]
        public string DatabasePassword { get; set; }

        [Required]
        public string DatabaseName { get; set; }

        public bool NotificationEmailComment { get; set; }

        public bool NotificationEmailNewSubscribers { get; set; }

        public bool NotificationEmailViewFeature { get; set; }

        public bool NotificationApplicationEveryThing { get; set; }

        public bool NotificationApplicationSameAsEmail { get; set; }

        public bool NotificationApplicationNoPush { get; set; }
    }
}