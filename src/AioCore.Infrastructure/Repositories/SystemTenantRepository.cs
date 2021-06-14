using AioCore.Application.Repositories;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using AioCore.Infrastructure.DbContexts;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SystemTenantRepository : Repository<SystemTenant>, ISettingTenantRepository
    {
        private readonly AioCoreContext _context;

        public SystemTenantRepository(AioCoreContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<SystemTenant> GetAsync(Guid id)
        {
            return await _context.SystemTenants.FindAsync(id);
        }
    }
}