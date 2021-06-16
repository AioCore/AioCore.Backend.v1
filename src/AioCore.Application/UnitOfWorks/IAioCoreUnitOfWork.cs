using AioCore.Application.Repositories;
using AioCore.Domain.SettingAggregatesModel.SettingActionAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingDomAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingFeatureAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingFieldAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingFilterAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingFormAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingLayoutAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingViewAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemApplicationAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemBinaryAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemGroupAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemPermissionAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemPermissionSetAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemPolicyAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;

namespace AioCore.Application.UnitOfWorks
{
    public interface IAioCoreUnitOfWork : IUnitOfWork
    {
        IRepository<SettingAction> SettingActions { get; }
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
