using AioCore.Domain.CoreEntities;
using AioCore.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Package.DatabaseManagement;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.UnitOfWorks;
using AioCore.Application.Services;

namespace AioCore.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TenantService> _logger;

        public TenantService(
              IAioCoreUnitOfWork aioCoreUnitOfWork
            , IHttpContextAccessor httpContextAccessor
            , IServiceProvider serviceProvider
            , ILogger<TenantService> logger
        )
        {
            _aioCoreUnitOfWork = aioCoreUnitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Guid? GetCurrentTenantId()
        {
            if (_httpContextAccessor.HttpContext != null &&
                Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst("tenant")?.Value, out var tenantId))
            {
                return Guid.Empty.Equals(tenantId) ? null : tenantId;
            }

            return null;
        }

        public async Task<SystemTenant> CreateTenantAsync(SystemTenant systemTenant, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _aioCoreUnitOfWork.SystemTenants.AddAsync(systemTenant, cancellationToken);
                await _aioCoreUnitOfWork.SaveChangesAsync(cancellationToken);
                var dbInfo = DatabaseSettings.Parse(item.DatabaseSettingsJson);

                //create database
                _httpContextAccessor.HttpContext?.User.AddIdentity(new ClaimsIdentity(new List<Claim>
                {
                    new("tenant_creating", item.Id.ToString()),
                    new("schema_creating", dbInfo?.Schema ?? "dbo")
                }));
                await _serviceProvider.GetRequiredService<AioDynamicContext>().Database.MigrateAsync(cancellationToken);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TenantService)}.{nameof(CreateTenantAsync)} EXCEPTION");
                throw;
            }
        }

        public async Task<SystemTenant> UpdateTenantAsync(SystemTenant systemTenant, CancellationToken cancellationToken)
        {
            try
            {
                var item = _aioCoreUnitOfWork.SystemTenants.Update(systemTenant);
                var result = await _aioCoreUnitOfWork.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    var dbInfo = DatabaseSettings.Parse(item.DatabaseSettingsJson);
                    //create database
                    _httpContextAccessor.HttpContext?.User.AddIdentity(new ClaimsIdentity(new List<Claim>
                    {
                        new("tenant_creating", item.Id.ToString()),
                        new("schema_creating", dbInfo?.Schema ?? "dbo")
                    }));
                    await _serviceProvider.GetRequiredService<AioDynamicContext>().Database.MigrateAsync(cancellationToken);
                }
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TenantService)}.{nameof(UpdateTenantAsync)} EXCEPTION");
                throw;
            }
        }
    }
}