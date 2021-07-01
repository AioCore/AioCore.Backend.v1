using AioCore.Domain.CoreEntities;
using AioCore.Infrastructure.DbContexts;
using System;
using System.Threading.Tasks;
using AioCore.Infrastructure.Repositories.Abstracts;

namespace AioCore.Infrastructure.Repositories
{
    public class SystemTenantRepository : Repository<SystemTenant>, ISettingTenantRepository
    {
        private readonly AioCoreContext _context;

        public SystemTenantRepository(AioCoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SystemTenant> GetAsync(Guid id)
        {
            return await _context.SystemTenants.FindAsync(id);
        }
    }
}