using Microsoft.AspNetCore.Http;
using Package.DatabaseManagement;
using System;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;

namespace AioCore.Application.Services.Implements
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
            var tenantId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirst("tenant").Value);
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
    }
}
