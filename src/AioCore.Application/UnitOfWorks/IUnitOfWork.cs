using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollbackTransaction();
        DbConnection GetDbConnection();
        IDbContextTransaction GetCurrentTransaction();
        IExecutionStrategy CreateExecutionStrategy();
    }
}