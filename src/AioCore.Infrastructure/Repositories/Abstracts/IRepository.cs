using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AioCore.Infrastructure.Repositories.Abstracts
{
    public interface IRepository { }

    public interface IRepository<TEntity> : IRepository, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IAsyncEnumerable<TEntity>, IListSource, IInfrastructure<IServiceProvider> where TEntity : class
    {
        TEntity Add([NotNull] TEntity entity);

        ValueTask<TEntity> AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

        void AddRange([NotNull] IEnumerable<TEntity> entities);

        void AddRange([NotNull] params TEntity[] entities);

        Task AddRangeAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        Task AddRangeAsync([NotNull] params TEntity[] entities);

        IAsyncEnumerable<TEntity> AsAsyncEnumerable();

        IQueryable<TEntity> AsQueryable();

        void AttachRange([NotNull] IEnumerable<TEntity> entities);

        void AttachRange([NotNull] params TEntity[] entities);

        TEntity Find(params object[] keyValues);

        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);

        ValueTask<TEntity> FindAsync(params object[] keyValues);

        TEntity Remove([NotNull] TEntity entity);

        void RemoveRange([NotNull] params TEntity[] entities);

        void RemoveRange([NotNull] IEnumerable<TEntity> entities);

        TEntity Update([NotNull] TEntity entity);

        void UpdateRange([NotNull] params TEntity[] entities);

        void UpdateRange([NotNull] IEnumerable<TEntity> entities);

        void Delete(Guid id);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}