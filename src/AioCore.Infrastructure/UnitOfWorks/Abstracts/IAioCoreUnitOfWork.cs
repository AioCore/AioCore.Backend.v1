using AioCore.Domain.CoreEntities;
using AioCore.Infrastructure.Repositories.Abstracts;

namespace AioCore.Infrastructure.UnitOfWorks.Abstracts
{
    public interface IAioCoreUnitOfWork : IUnitOfWork
    {
        IRepository<SettingAction> SettingActions { get; }
        IRepository<SettingActionStep> SettingActionSteps { get; }
        IRepository<SettingComponent> SettingComponents { get; }
        IRepository<SettingDom> SettingDoms { get; }
        IRepository<SettingEntityType> SettingEntityTypes { get; }
        IRepository<SettingFeature> SettingFeatures { get; }
        IRepository<SettingField> SettingFields { get; }
        IRepository<SettingForm> SettingForms { get; }
        IRepository<SettingLayout> SettingLayouts { get; }
        IRepository<SettingView> SettingViews { get; }
        IRepository<SystemApplication> SystemApplications { get; }
        IRepository<SystemGroup> SystemGroups { get; }
        IRepository<SystemPermissionSet> SystemPermissionSets { get; }
        IRepository<SystemPermission> SystemPermissions { get; }
        IRepository<SystemPolicy> SystemPolicies { get; }
        IRepository<SystemTenant> SystemTenants { get; }
        IRepository<SystemTenantApplication> SystemTenantApplications { get; }
        IRepository<SystemUser> SystemUsers { get; }
        IRepository<SystemUserGroup> SystemUserGroups { get; }
        IRepository<SystemUserPolicy> SystemUserPolicies { get; }
        IRepository<SystemBinary> SystemBinaries { get; }
        IRepository<SettingFilter> SettingFilters { get; }
    }
}