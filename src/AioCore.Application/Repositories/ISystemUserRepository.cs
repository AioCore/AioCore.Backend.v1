using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using System;
using System.Threading.Tasks;

namespace AioCore.Application.Repositories
{
    public interface ISystemUserRepository : IRepository<SystemUser>
    {
        Task<SystemUser> GetAsync(Guid id);
    }
}