using AioCore.Application.Repositories;
using AioCore.Application.UnitOfWorks;
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
using AioCore.Infrastructure.DbContexts;

namespace AioCore.Infrastructure.UnitOfWorks
{
    public class AioCoreUnitOfWork : UnitOfWork, IAioCoreUnitOfWork
    {
        public IRepository<SettingAction> SettingActions { get; set; }
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
