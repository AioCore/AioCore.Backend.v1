using System;
using System.Threading.Tasks;
using AioCore.Domain.CoreEntities;

namespace AioCore.Application.Repositories
{
    public interface ISettingTenantRepository : IRepository<SystemTenant>
    {
        Task<SystemTenant> GetAsync(Guid id);
    }
}