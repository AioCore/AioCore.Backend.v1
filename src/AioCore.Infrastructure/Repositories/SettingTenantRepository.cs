using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public SettingTenant Add(SettingTenant tenant)
        {
            return _context.SettingTenants.Add(tenant).Entity;
        }

        public void Update(SettingTenant tenant)
        {
            _context.Entry(tenant).State = EntityState.Modified;
        }

        public async Task<SettingTenant> GetAsync(Guid tenantId)
        {
            var tenant = await _context.SettingTenants.FindAsync(tenantId);
            return tenant;
        }

        public IQueryable<SettingTenant> GetAsync(int skip, int take, string query)
        {
            var specs = SettingTenantSpecs.SearchTenantByName(query);
            var tenants = _context.SettingTenants.Where(specs.Predicate).Skip(skip).Take(take);
            return tenants;
        }

        public async Task<long> LongCountAsync()
        {
            return await _context.SettingTenants.LongCountAsync();
        }
    }
}