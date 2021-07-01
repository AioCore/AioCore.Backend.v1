using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Infrastructure.Repositories.Abstracts;

namespace AioCore.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        Type IQueryable.ElementType => (_dbSet as IQueryable).ElementType;

        Expression IQueryable.Expression => (_dbSet as IQueryable).Expression;

        IQueryProvider IQueryable.Provider => (_dbSet as IQueryable).Provider;

        bool IListSource.ContainsListCollection => (_dbSet as IListSource).ContainsListCollection;

        IServiceProvider IInfrastructure<IServiceProvider>.Instance => (_dbSet as IInfrastructure<IServiceProvider>).Instance;

        IEnumerator IEnumerable.GetEnumerator() => (_dbSet as IEnumerable).GetEnumerator();

        IList IListSource.GetList() => (_dbSet as IListSource).GetList();

        public virtual TEntity Add([NotNull] TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public virtual async ValueTask<TEntity> AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            return (await _dbSet.AddAsync(entity, cancellationToken)).Entity;
        }

        public virtual void AddRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void AddRange([NotNull] params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual async Task AddRangeAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task AddRangeAsync([NotNull] params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual IAsyncEnumerable<TEntity> AsAsyncEnumerable()
        {
            return _dbSet.AsAsyncEnumerable();
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual void AttachRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.AttachRange(entities);
        }

        public virtual void AttachRange([NotNull] params TEntity[] entities)
        {
            _dbSet.AttachRange(entities);
        }

        public virtual void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual async ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(keyValues, cancellationToken);
        }

        public virtual async ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public virtual IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return (_dbSet as IAsyncEnumerable<TEntity>).GetAsyncEnumerator(cancellationToken);
        }

        public virtual IEnumerator<TEntity> GetEnumerator()
        {
            return (_dbSet as IEnumerable<TEntity>).GetEnumerator();
        }

        public virtual TEntity Remove([NotNull] TEntity entity)
        {
            return _dbSet.Remove(entity).Entity;
        }

        public virtual void RemoveRange([NotNull] params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void RemoveRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual TEntity Update([NotNull] TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public virtual void UpdateRange([NotNull] params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void UpdateRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}