using AioCore.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public Type ElementType => (_dbSet as IQueryable).ElementType;

        public Expression Expression => (_dbSet as IQueryable).Expression;

        public IQueryProvider Provider => (_dbSet as IQueryable).Provider;

        public TEntity Add([NotNull] TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public async ValueTask<TEntity> AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await _dbSet.AddAsync(entity, cancellationToken);
            return result.Entity;
        }

        public void AddRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void AddRange([NotNull] params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task AddRangeAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public async Task AddRangeAsync([NotNull] params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public IAsyncEnumerable<TEntity> AsAsyncEnumerable()
        {
            return _dbSet.AsAsyncEnumerable();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public void AttachRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.AttachRange(entities);
        }

        public void AttachRange([NotNull] params TEntity[] entities)
        {
            _dbSet.AttachRange(entities);
        }

        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public async ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(keyValues, cancellationToken);
        }

        public async ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return (_dbSet as IAsyncEnumerable<TEntity>).GetAsyncEnumerator(cancellationToken);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return (_dbSet as IEnumerable<TEntity>).GetEnumerator();
        }

        public TEntity Remove([NotNull] TEntity entity)
        {
            return _dbSet.Remove(entity).Entity;
        }

        public void RemoveRange([NotNull] params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void RemoveRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public TEntity Update([NotNull] TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public void UpdateRange([NotNull] params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void UpdateRange([NotNull] IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_dbSet as IEnumerable).GetEnumerator();
        }
    }
}
