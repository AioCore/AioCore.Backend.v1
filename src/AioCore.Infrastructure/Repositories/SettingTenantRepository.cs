using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class SettingTenantRepository : ISettingTenantRepository
    {
        private readonly AioCoreContext _context;

        public SettingTenantRepository(AioCoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<SettingTenant> GetAsync(Guid id)
        {
            return await _context.SettingTenants.FindAsync(id);
        }

        public SettingTenant Add(SettingTenant tenant)
        {
            return _context.SettingTenants.Add(tenant).Entity;
        }

        public void Update(SettingTenant tenant)
        {
            _context.Entry(tenant).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var tenant = _context.SettingTenants.Find(id);
            _context.Entry(tenant).State = EntityState.Deleted;
        }
    }
}