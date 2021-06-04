using System;
using System.Threading.Tasks;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate
{
    public interface ISettingTenantRepository : IRepository<SystemTenant>
    {
        Task<SystemTenant> GetAsync(Guid id);

        SystemTenant Add(SystemTenant tenant);

        void Update(SystemTenant tenant);

        void Delete(Guid id);
    }
}