using AioCore.Shared.Seedwork;

namespace AioCore.Domain.AggregatesModel.SecurityUserAggregate
{
    public interface ISecurityUserRepository : IRepository<SecurityUser>
    {
        SecurityUser Add(SecurityUser user);

        void Update(SecurityUser user);
    }
}