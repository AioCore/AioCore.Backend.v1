using AioCore.Shared.Seedwork;
using System;
using System.Threading.Tasks;

namespace AioCore.Domain.AggregatesModel.SecurityUserAggregate
{
    public interface ISecurityUserRepository : IRepository<SecurityUser>
    {
        Task<SecurityUser> GetAsync(Guid id);

        SecurityUser Add(SecurityUser user);

        void Update(SecurityUser user);

        void Delete(Guid id);
    }
}