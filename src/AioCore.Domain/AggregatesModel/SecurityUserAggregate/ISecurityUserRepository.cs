using AioCore.Shared.Seedwork;
using System;

namespace AioCore.Domain.AggregatesModel.SecurityUserAggregate
{
    public interface ISecurityUserRepository : IRepository<SecurityUser>
    {
        SecurityUser Add(SecurityUser user);

        void Update(SecurityUser user);

        void Delete(Guid id);
    }
}