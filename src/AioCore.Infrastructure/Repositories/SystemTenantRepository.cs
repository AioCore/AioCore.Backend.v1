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

        public string GetConnectionString(SystemTenant tenant)
        {
            if (tenant == null) throw new ArgumentNullException(nameof(tenant));
            
            if(!Enum.TryParse<DatabaseType>(tenant.DatabaseType, out var databaseType))
            {
                databaseType = DatabaseType.MsSql;
            }

            return databaseType switch
            {
                DatabaseType.PostgresSql => $"Host={tenant.Server};User ID={tenant.User};Password={tenant.Password};Database={tenant.Database};Pooling=true;",
                _ => $"Server={tenant.Server};User ID={tenant.User};Password={tenant.Password};Database={tenant.Database};Pooling=true;",
            };
        }
    }
}