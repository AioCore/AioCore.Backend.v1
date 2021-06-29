using AioCore.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Package.DatabaseManagement;
using System;
using System.Threading.Tasks;
using System.Threading;
using AioCore.Domain.DynamicEntities;

namespace AioCore.Infrastructure.DbContexts
{
    public class AioDynamicContext : SchemaDbContext
    {
        private readonly IServiceProvider _serviceProvider;

        public AioDynamicContext(DbContextOptions<AioDynamicContext> options, IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public DbSet<DynamicEntity> DynamicEntities { get; set; }
        public DbSet<DynamicAttribute> DynamicAttributes { get; set; }

        public DbSet<DynamicDateValue> DynamicDateValues { get; set; }
        public DbSet<DynamicFloatValue> DynamicFloatValues { get; set; }
        public DbSet<DynamicGuidValue> DynamicGuidValues { get; set; }
        public DbSet<DynamicIntegerValue> DynamicIntegerValues { get; set; }
        public DbSet<DynamicStringValue> DynamicStringValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DynamicAttributeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicDateValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicFloatValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicGuidValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicIntegerValueTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DynamicStringValueTypeConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveEntitiesAsync(base.SaveChangesAsync, _serviceProvider, cancellationToken);
        }
    }
}
