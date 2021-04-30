using AioCore.Shared.Seedwork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AioCore.Domain.AggregatesModel.SettingTenantAggregate
{
    public interface ISettingTenantRepository : IRepository<SettingTenant>
    {
        SettingTenant Add(SettingTenant tenant);

        void Update(SettingTenant tenant);

        Task<SettingTenant> GetAsync(Guid tenantId);

        IQueryable<SettingTenant> GetAsync(int skip, int take, string query);

        Task<long> LongCountAsync();
    }
}