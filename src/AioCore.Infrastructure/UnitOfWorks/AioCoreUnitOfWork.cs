using AioCore.Domain.CoreEntities;
using AioCore.Infrastructure.DbContexts;
using AioCore.Infrastructure.Repositories.Abstracts;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;

namespace AioCore.Infrastructure.UnitOfWorks
{
    public class AioCoreUnitOfWork : UnitOfWork, IAioCoreUnitOfWork
    {
        public IRepository<SettingAction> SettingActions { get; set; }
        public IRepository<SettingActionStep> SettingActionSteps { get; set; }
        public IRepository<SettingComponent> SettingComponents { get; set; }
        public IRepository<SettingDom> SettingDoms { get; set; }
        public IRepository<SettingEntityType> SettingEntityTypes { get; set; }
        public IRepository<SettingFeature> SettingFeatures { get; set; }
        public IRepository<SettingField> SettingFields { get; set; }
        public IRepository<SettingForm> SettingForms { get; set; }
        public IRepository<SettingLayout> SettingLayouts { get; set; }
        public IRepository<SettingView> SettingViews { get; set; }
        public IRepository<SystemApplication> SystemApplications { get; set; }
        public IRepository<SystemGroup> SystemGroups { get; set; }
        public IRepository<SystemPermissionSet> SystemPermissionSets { get; set; }
        public IRepository<SystemPermission> SystemPermissions { get; set; }
        public IRepository<SystemPolicy> SystemPolicies { get; set; }
        public IRepository<SystemTenant> SystemTenants { get; set; }
        public IRepository<SystemTenantApplication> SystemTenantApplications { get; set; }
        public IRepository<SystemUser> SystemUsers { get; set; }
        public IRepository<SystemUserGroup> SystemUserGroups { get; set; }
        public IRepository<SystemUserPolicy> SystemUserPolicies { get; set; }
        public IRepository<SystemBinary> SystemBinaries { get; set; }
        public IRepository<SettingFilter> SettingFilters { get; set; }

        public AioCoreUnitOfWork(AioCoreContext context) : base(context)
        {
        }
    }
}