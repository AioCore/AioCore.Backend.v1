using AioCore.Domain.AggregatesModel.DynamicAggregate;
using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using AioCore.Infrastructure.EntityTypeConfigurations;
using AioCore.Shared.Seedwork;
using Microsoft.EntityFrameworkCore;
using Package.DatabaseManagement;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure
{
    public class AioDynamicContext : SchemaDbContext, IUnitOfWork
    {
        public AioDynamicContext(DbContextOptions<AioDynamicContext> options, ISchemaDbContext schema)
            : base(options, schema)
        {
        }

        public DbSet<DynamicEntity> DynamicEntities { get; set; }
        public DbSet<DynamicAttribute> DynamicAttributes { get; set; }
        
        public DbSet<DynamicBinary> DynamicBinaries { get; set; }
        public DbSet<DynamicDateValue> DynamicDateValues { get; set; }
        public DbSet<DynamicFloatValue> DynamicFloatValues { get; set; }
        public DbSet<DynamicGuidValue> DynamicGuidValues { get; set; }
        public DbSet<DynamicIntegerValue> DynamicIntegerValues { get; set; }
        public DbSet<DynamicStringValue> DynamicStringValues { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }

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
