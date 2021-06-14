using AioCore.Application.Repositories;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using AioCore.Infrastructure.DbContexts;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SystemUserRepository : Repository<SystemUser>, ISystemUserRepository
    {
        private readonly AioCoreContext _context;

        public SystemUserRepository(AioCoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SystemUser> GetAsync(Guid id)
        {
            return await _context.SystemUsers.FindAsync(id);
        }
    }
}