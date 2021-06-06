using AioCore.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Package.DatabaseManagement;
using AioCore.Domain.DynamicAggregatesModel;
using AioCore.Domain.SystemAggregatesModel.SystemBinaryAggregate;

namespace AioCore.Infrastructure
{
    public class AioDynamicContext : SchemaDbContext
    {
        public AioDynamicContext(DbContextOptions<AioDynamicContext> options, ISchemaDbContext schema)
            : base(options, schema)
        {
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
    }
}
