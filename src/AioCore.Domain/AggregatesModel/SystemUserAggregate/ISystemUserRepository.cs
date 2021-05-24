using System;
using System.Threading.Tasks;
using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SystemUserAggregate
{
    public interface ISystemUserRepository : IRepository<SystemUser>
    {
        Task<SystemUser> GetAsync(Guid id);

        SystemUser Add(SystemUser user);

        void Update(SystemUser user);

        void Delete(Guid id);
    }
}