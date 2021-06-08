using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Services
{
    public interface ITenantService
    {
        Task<SystemTenant> CreateTenantAsync(SystemTenant systemTenant, CancellationToken cancellationToken);
    }
}
