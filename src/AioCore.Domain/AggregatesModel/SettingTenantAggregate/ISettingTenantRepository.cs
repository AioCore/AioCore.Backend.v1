using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SettingTenantAggregate
{
    public interface ISettingTenantRepository : IRepository<SettingTenant>
    {
        SettingTenant Add(SettingTenant tenant);

        void Update(SettingTenant tenant);
    }
}