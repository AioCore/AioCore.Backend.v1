using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AioCore.Shared.Exceptions;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using AioCore.Infrastructure.DbContexts;
using Package.DatabaseManagement;
using AioCore.Application.UnitOfWorks;

namespace AioCore.Infrastructure.UnitOfWorks
{
    public class AioDynamicUnitOfWorkFactory : IAioDynamicUnitOfWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;

        public AioDynamicUnitOfWorkFactory(
              IServiceProvider serviceProvider
            , IHttpContextAccessor httpContextAccessor
            , IAioCoreUnitOfWork aioCoreUnitOfWork)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
            _aioCoreUnitOfWork = aioCoreUnitOfWork;
        }

        public async Task<IAioDynamicUnitOfWork> CreateUnitOfWorkAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var tenant = await _aioCoreUnitOfWork.SystemTenants.FindAsync(new object[] { tenantId }, cancellationToken);
            if (tenant == null)
            {
                throw new AioCoreException("Tenant not found");
            }
            var dbInfo = DatabaseSettings.Parse(tenant.DatabaseSettingsJson);
            _httpContextAccessor.HttpContext?.User.AddIdentity(new ClaimsIdentity(new List<Claim>
            {
                new("tenant_creating", tenant.Id.ToString()),
                new("schema_creating", dbInfo?.Schema ?? string.Empty)
            }));

            var dbContext = _serviceProvider.GetRequiredService<AioDynamicContext>();
            await dbContext.Database.MigrateAsync(cancellationToken);
            return _serviceProvider.GetRequiredService<IAioDynamicUnitOfWork>();
        }
    }
}