using Microsoft.AspNetCore.Http;
using Package.DatabaseManagement;
using System;
using AioCore.Application.Repositories;
using AioCore.Application.Services;

namespace AioCore.Infrastructure.Services
{
    public class DatabaseSettingsService : IDatabaseInfoService
    {
        private readonly ISettingTenantRepository _settingTenantRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public DatabaseSettingsService(ISettingTenantRepository settingTenantRepository, IHttpContextAccessor contextAccessor)
        {
            _settingTenantRepository = settingTenantRepository;
            _contextAccessor = contextAccessor;
        }

        public DatabaseSettings GetDatabaseInfo()
        {
            if (!Guid.TryParse(FindClaim("tenant_creating") ?? FindClaim("tenant"), out var tenantId))
                return DatabaseSettings.Parse("{}");
            var tenant = _settingTenantRepository.GetAsync(tenantId).GetAwaiter().GetResult();
            return DatabaseSettings.Parse(tenant?.DatabaseSettingsJson);
        }

        private string FindClaim(string key)
        {
            return _contextAccessor.HttpContext?.User.FindFirst(key)?.Value;
        }
    }
}