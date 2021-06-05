using AioCore.Application.Repositories;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.SettingAggregatesModel.SettingActionAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingDomAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingFeatureAggregate;
using AioCore.Domain.SettingAggregatesModel.SettingFieldAggregate;
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
using AioCore.Infrastructure.Repositories;

namespace AioCore.Infrastructure.UnitOfWorks
{
    public class AioCoreUnitOfWork : UnitOfWork, IAioCoreUnitOfWork
    {
        public IRepository<SettingAction> SettingActions { get; }
        public IRepository<SettingComponent> SettingComponents { get; }
        public IRepository<SettingDom> SettingDoms { get; }
        public IRepository<SettingEntityType> SettingEntityTypes { get; }
        public IRepository<SettingFeature> SettingFeatures { get; }
        public IRepository<SettingField> SettingFields { get; }
        public IRepository<SettingForm> SettingForms { get; }
        public IRepository<SettingLayout> SettingLayouts { get; }
        public IRepository<SettingView> SettingViews { get; }
        public IRepository<SystemApplication> SystemApplications { get; }
        public IRepository<SystemGroup> SystemGroups { get; }
        public IRepository<SystemPermissionSet> SystemPermissionSets { get; }
        public IRepository<SystemPermission> SystemPermissions { get; }
        public IRepository<SystemPolicy> SystemPolicies { get; }
        public IRepository<SystemTenant> SystemTenants { get; }
        public IRepository<SystemTenantApplication> SystemTenantApplications { get; }
        public IRepository<SystemUser> SystemUsers { get; }
        public IRepository<SystemUserGroup> SystemUserGroups { get; }
        public IRepository<SystemUserPolicy> SystemUserPolicies { get; }

        public IRepository<SystemBinary> SystemBinaries { get; }

        public AioCoreUnitOfWork(AioCoreContext context) : base(context)
        {
            SettingActions = new RepositoryImpl<SettingAction>(context);
            SettingComponents = new RepositoryImpl<SettingComponent>(context);
            SettingDoms = new RepositoryImpl<SettingDom>(context);
            SettingEntityTypes = new RepositoryImpl<SettingEntityType>(context);
            SettingFeatures = new RepositoryImpl<SettingFeature>(context);
            SettingFields = new RepositoryImpl<SettingField>(context);
            SettingForms = new RepositoryImpl<SettingForm>(context);
            SettingLayouts = new RepositoryImpl<SettingLayout>(context);
            SettingViews = new RepositoryImpl<SettingView>(context);
            SystemApplications = new RepositoryImpl<SystemApplication>(context);
            SystemGroups = new RepositoryImpl<SystemGroup>(context);
            SystemPermissionSets = new RepositoryImpl<SystemPermissionSet>(context);
            SystemPermissions = new RepositoryImpl<SystemPermission>(context);
            SystemPolicies = new RepositoryImpl<SystemPolicy>(context);
            SystemTenants = new RepositoryImpl<SystemTenant>(context);
            SystemTenantApplications = new RepositoryImpl<SystemTenantApplication>(context);
            SystemUsers = new RepositoryImpl<SystemUser>(context);
            SystemUserGroups = new RepositoryImpl<SystemUserGroup>(context);
            SystemUserPolicies = new RepositoryImpl<SystemUserPolicy>(context);
            SystemBinaries = new RepositoryImpl<SystemBinary>(context);
        }
    }
}
