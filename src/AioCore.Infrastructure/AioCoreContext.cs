using AioCore.Domain.AggregatesModel.SettingActionAggregate;
using AioCore.Domain.AggregatesModel.SettingComponentAggregate;
using AioCore.Domain.AggregatesModel.SettingDomAggregate;
using AioCore.Domain.AggregatesModel.SettingEntityAggregate;
using AioCore.Domain.AggregatesModel.SettingFeatureAggregate;
using AioCore.Domain.AggregatesModel.SettingFieldAggregate;
using AioCore.Domain.AggregatesModel.SettingFormAggregate;
using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using AioCore.Domain.AggregatesModel.SettingViewAggregate;
using AioCore.Domain.AggregatesModel.SystemApplicationAggregate;
using AioCore.Domain.AggregatesModel.SystemGroupAggregate;
using AioCore.Domain.AggregatesModel.SystemPermissionAggregate;
using AioCore.Domain.AggregatesModel.SystemPermissionSetAggregate;
using AioCore.Domain.AggregatesModel.SystemPolicyAggregate;
using AioCore.Domain.AggregatesModel.SystemTenantAggregate;
using AioCore.Domain.AggregatesModel.SystemUserAggregate;
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

        public DbSet<SettingAction> SettingActions { get; set; }

        public DbSet<SettingComponent> SettingComponents { get; set; }

        public DbSet<SettingDom> SettingDoms { get; set; }

        public DbSet<SettingEntityType> SettingEntityTypes { get; set; }

        public DbSet<SettingFeature> SettingFeatures { get; set; }

        public DbSet<SettingField> SettingFields { get; set; }

        public DbSet<SettingForm> SettingForms { get; set; }

        public DbSet<SettingLayout> SettingLayouts { get; set; }

        public DbSet<SettingView> SettingViews { get; set; }

        public DbSet<SystemApplication> SystemApplications { get; set; }

        public DbSet<SystemGroup> SystemGroups { get; set; }

        public DbSet<SystemPermissionSet> SystemPermissionSets { get; set; }

        public DbSet<SystemPermission> SystemPermissions { get; set; }

        public DbSet<SystemPolicy> SystemPolicies { get; set; }

        public DbSet<SystemTenant> SystemTenants { get; set; }

        public DbSet<SystemTenantApplication> SystemTenantApplications { get; set; }

        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<SystemUserGroup> SystemUserGroups { get; set; }

        public DbSet<SystemUserPolicy> SystemUserPolicies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SystemGroupTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SystemPermissionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SystemPermissionSetTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SystemPolicyTypeConfiguration());

            modelBuilder.ApplyConfiguration(new SettingActionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingComponentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingDomTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingFeatureTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingFieldTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingFormTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SettingLayoutTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SystemTenantTypeConfiguration());
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