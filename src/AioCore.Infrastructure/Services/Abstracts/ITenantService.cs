using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.CoreEntities;

namespace AioCore.Infrastructure.Services.Abstracts
{
    public interface ITenantService
    {
        Guid? GetCurrentTenantId();

        Task<SystemTenant> CreateTenantAsync(SystemTenant systemTenant, CancellationToken cancellationToken);

        Task<SystemTenant> UpdateTenantAsync(SystemTenant systemTenant, CancellationToken cancellationToken);
    }
}