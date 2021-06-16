using System;
using System.Threading.Tasks;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;

namespace AioCore.Application.Repositories
{
    public interface ISettingTenantRepository : IRepository<SystemTenant>
    {
        Task<SystemTenant> GetAsync(Guid id);
    }
}