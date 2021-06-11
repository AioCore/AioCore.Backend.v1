using AioCore.Application.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AioCore.Shared.Exceptions;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IAioDynamicUnitOfWork> CreateUnitOfWorkAsync(Guid tenantId)
        {
            var tenant = await _aioCoreUnitOfWork.SystemTenants.FindAsync(tenantId);
            if (tenant == null)
            {
                throw new AioCoreException("Tenant not found");
            }

            _httpContextAccessor.HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim>
            {
                new Claim("tenant_creating", tenant.Id.ToString()),
                new Claim("schema_creating", tenant.Schema)
            }));

            var dbContext = _serviceProvider.GetRequiredService<AioDynamicContext>();
            await dbContext.Database.MigrateAsync();
            return new AioDynamicUnitOfWork(dbContext);
        }
    }
}
