using AioCore.Application.Repositories;
using AioCore.Application.UnitOfWorks;
using AioCore.Infrastructure.Repositories;
using FastMember;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.UnitOfWorks
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            TypeInitialize.InitializeRepositories(this, context);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return _currentTransaction;
            _currentTransaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _currentTransaction;
        }

        public DbConnection GetDbConnection()
        {
            return _context.Database.GetDbConnection();
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return _context.Database.CreateExecutionStrategy();
        }

        private static class TypeInitialize
        {
            private static readonly ConcurrentDictionary<Type, List<Member>> _members = new ConcurrentDictionary<Type, List<Member>>();
            private static readonly ConcurrentDictionary<string, Type> _memberTypes = new ConcurrentDictionary<string, Type>();

            public static void InitializeRepositories<T>(T uow, DbContext context) where T : UnitOfWork
            {
                var uowType = uow.GetType();
                var accessor = TypeAccessor.Create(uowType);
                var members = _members.GetOrAdd(uowType, _ => accessor.GetMembers().Where(t => typeof(IRepository).IsAssignableFrom(t.Type) && t.Type.IsGenericType).ToList());
                foreach (var member in members)
                {
                    var implType = _memberTypes.GetOrAdd($"{uowType.Name}_{member.Name}", _ =>
                    {
                        var genericType = member.Type.GetGenericArguments()[0];
                        return typeof(Repository<>).MakeGenericType(genericType);
                    });
                    accessor[uow, member.Name] = Activator.CreateInstance(implType, context);
                }
            }
        }
    }
}
