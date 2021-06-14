using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.UnitOfWorks
{
    public interface IAioDynamicUnitOfWorkFactory
    {
        Task<IAioDynamicUnitOfWork> CreateUnitOfWorkAsync(Guid tenantId, CancellationToken cancellationToken = default);
    }
}
