using AioCore.Domain.AggregatesModel.SystemTenantAggregate;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SystemTenantRepository : ISettingTenantRepository
    {
        private readonly AioCoreContext _context;

        public SystemTenantRepository(AioCoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<SystemTenant> GetAsync(Guid id)
        {
            return await _context.SystemTenants.FindAsync(id);
        }

        public SystemTenant Add(SystemTenant tenant)
        {
            return _context.SystemTenants.Add(tenant).Entity;
        }

        public void Update(SystemTenant tenant)
        {
            _context.Entry(tenant).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var tenant = _context.SystemTenants.Find(id);
            _context.Entry(tenant).State = EntityState.Deleted;
        }
    }
}