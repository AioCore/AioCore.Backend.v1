using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Package.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;
        private readonly IElasticsearchService _elasticsearchService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TenantService> _logger;

        public TenantService(
              IAioCoreUnitOfWork aioCoreUnitOfWork
            , IElasticsearchService elasticsearchService
            , IHttpContextAccessor httpContextAccessor
            , IServiceProvider serviceProvider
            , ILogger<TenantService> logger
        )
        {
            _aioCoreUnitOfWork = aioCoreUnitOfWork;
            _elasticsearchService = elasticsearchService;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Guid? GetCurrentTenant()
        {
            if (Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst("tenant")?.Value, out var tenantId))
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
                await _elasticsearchService.IndexAsync(item);

                //create database
                _httpContextAccessor.HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("tenant_creating", item.Id.ToString()),
                    new Claim("schema_creating", item.Schema)
                }));
                await _serviceProvider.GetRequiredService<AioDynamicContext>().Database.MigrateAsync(cancellationToken: cancellationToken);
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(TenantService)}.{nameof(CreateTenantAsync)} EXCEPTION");
                throw;
            }
        }
    }
}
