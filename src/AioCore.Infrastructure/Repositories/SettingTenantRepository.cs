using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;

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
    }
}