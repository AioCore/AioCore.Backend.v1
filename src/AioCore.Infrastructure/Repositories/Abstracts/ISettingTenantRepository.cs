using System;
using System.Threading.Tasks;
using AioCore.Domain.CoreEntities;

namespace AioCore.Infrastructure.Repositories.Abstracts
{
    public interface ISettingTenantRepository : IRepository<SystemTenant>
    {
        Task<SystemTenant> GetAsync(Guid id);
    }
}