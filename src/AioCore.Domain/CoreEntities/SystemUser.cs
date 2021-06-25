using System;
using System.Collections.Generic;
using AioCore.Domain.Common;
using Nest;
using Package.Extensions;

namespace AioCore.Domain.CoreEntities
{
    public class SystemUser : Entity, IAggregateRoot
    {
        [Text(Analyzer = "vi", SearchAnalyzer = "vi")]
        public string Name { get; set; }

        [Keyword]
        public string Account { get; set; }

        [Keyword]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public virtual ICollection<SystemUserGroup> Groups { get; set; }

        public virtual ICollection<SystemUserPolicy> Policies { get; set; }

        public virtual ICollection<SystemTenant> Tenants { get; set; }
    }
}