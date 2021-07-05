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
            if (Guid.TryParse(FindClaim("tenant_creating") ?? FindClaim("tenant"), out var tenantId))
            {
                var tenant = _settingTenantRepository.GetAsync(tenantId).GetAwaiter().GetResult();
                return DatabaseInfo.Parse(tenant?.DatabaseInfo);
            }
            return DatabaseInfo.Parse("{}");
        }

        private string FindClaim(string key)
        {
            return _contextAccessor.HttpContext.User.FindFirst(key)?.Value;
        }
    }
}