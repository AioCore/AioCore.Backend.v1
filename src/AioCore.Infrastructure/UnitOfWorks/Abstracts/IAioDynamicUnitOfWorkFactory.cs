using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.UnitOfWorks.Abstracts
{
    public interface IAioDynamicUnitOfWorkFactory
    {
        Task<IAioDynamicUnitOfWork> CreateUnitOfWorkAsync(Guid tenantId, CancellationToken cancellationToken = default);
    }
}