using AioCore.Shared.Seedwork;
using System;
using System.Threading.Tasks;

namespace AioCore.Domain.AggregatesModel.SettingTenantAggregate
{
    public interface ISettingTenantRepository : IRepository<SettingTenant>
    {
        Task<SettingTenant> GetAsync(Guid id);

        SettingTenant Add(SettingTenant tenant);

        void Update(SettingTenant tenant);

        void Delete(Guid id);
    }
}