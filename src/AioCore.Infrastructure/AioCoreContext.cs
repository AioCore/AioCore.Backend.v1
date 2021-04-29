using AioCore.Domain.AggregatesModel.DynamicDateAggregate;
using AioCore.Domain.AggregatesModel.DynamicEntityAggregate;
using AioCore.Domain.AggregatesModel.DynamicFloatAggregate;
using AioCore.Domain.AggregatesModel.DynamicGuidAggregate;
using AioCore.Domain.AggregatesModel.DynamicIntegerAggregate;
using AioCore.Domain.AggregatesModel.DynamicStringAggregate;
using AioCore.Infrastructure.EntityTypeConfigurations;
using AioCore.Shared.Extensions;
using AioCore.Shared.Seedwork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure
{
    public sealed class AioCoreContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public AioCoreContext(DbContextOptions<AioCoreContext> options) : base(options)
        {
        }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public AioCoreContext(DbContextOptions<AioCoreContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine($"{nameof(AioCoreContext)}::ctor ->" + GetHashCode());
        }

        public DbSet<DynamicDateAttribute> DynamicDateAttributes { get; set; }

        public DbSet<DynamicDateValue> DynamicDateValues { get; set; }

        public DbSet<DynamicEntity> DynamicEntities { get; set; }

        public DbSet<DynamicFloatAttribute> DynamicFloatAttributes { get; set; }

        public DbSet<DynamicFloatValue> DynamicFloatValues { get; set; }

        public DbSet<DynamicGuidAttribute> DynamicGuidAttributes { get; set; }

        public DbSet<DynamicGuidValue> DynamicGuidValues { get; set; }

        public DbSet<DynamicIntegerAttribute> DynamicIntegerAttributes { get; set; }

        public DbSet<DynamicIntegerValue> DynamicIntegerValues { get; set; }

        public DbSet<DynamicStringAttribute> DynamicStringAttributes { get; set; }

        public DbSet<DynamicStringValue> DynamicStringValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DynamicDateAttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicDateValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicFloatAttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicFloatValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicGuidAttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicGuidValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicIntegerAttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicIntegerValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicStringAttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicStringValueTypeConfiguration());

            modelBuilder.ApplyConfiguration(new SettingActionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingDomTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingFeatureTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingFieldTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingFormTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingTenantTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingViewTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

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
    }
}