using Microsoft.AspNetCore.Http;
using Package.DatabaseManagement;
using System;
using AioCore.Application.Repositories;
using AioCore.Application.Services;

namespace AioCore.Infrastructure.Services
{
    public class DatabaseInfoService : IDatabaseInfoService
    {
        private readonly ISettingTenantRepository _settingTenantRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public DatabaseInfoService(ISettingTenantRepository settingTenantRepository, IHttpContextAccessor contextAccessor)
        {
            _settingTenantRepository = settingTenantRepository;
            _contextAccessor = contextAccessor;
        }

        public DatabaseInfo GetDatabaseInfo()
        {
            var tenantId = Guid.Parse(FindClaim("tenant_creating") ?? FindClaim("tenant"));
            var tenant = _settingTenantRepository.GetAsync(tenantId).GetAwaiter().GetResult();
            if (!Enum.TryParse<DatabaseType>(tenant.DatabaseType, out var databaseType))
            {
                databaseType = DatabaseType.MsSql;
            }
            return new DatabaseInfo
            {
                User = tenant.User,
                Database = tenant.Database,
                Password = tenant.Password,
                Schema = tenant.Schema,
                Server = tenant.Server,
                DatabaseType = databaseType
            };
        }

        private string FindClaim(string key)
        {
            return _contextAccessor.HttpContext.User.FindFirst(key)?.Value;
        }
    }
}
