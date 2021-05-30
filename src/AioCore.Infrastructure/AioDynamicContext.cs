using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using AioCore.Domain.AggregatesModel.DynamicDateAggregate;
using AioCore.Domain.AggregatesModel.DynamicEntityAggregate;
using AioCore.Domain.AggregatesModel.DynamicFloatAggregate;
using AioCore.Domain.AggregatesModel.DynamicGuidAggregate;
using AioCore.Domain.AggregatesModel.DynamicIntegerAggregate;
using AioCore.Domain.AggregatesModel.DynamicStringAggregate;
using AioCore.Infrastructure.EntityTypeConfigurations;
using AioCore.Shared.Seedwork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Package.DatabaseManagement;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure
{
    public class AioDynamicContext : SchemaDbContext, IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public AioDynamicContext(DbContextOptions<AioDynamicContext> options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
        }

        public DbSet<DynamicBinary> DynamicBinaries { get; set; }

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

        public override string GetSchema()
        {
            return _serviceProvider?.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User.FindFirst("schema")?.Value;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }

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
        }
    }
}
