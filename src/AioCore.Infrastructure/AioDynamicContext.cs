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
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Infrastructure
{
    public class AioDynamicContext : DbContext, IUnitOfWork, IDbContextSchema
    {
        public string Schema { get; set; }

        public AioDynamicContext(
              DbContextOptions<AioDynamicContext> options
            , IHttpContextAccessor contextAccessor
        ) : base(options)
        {
            Schema = contextAccessor?.HttpContext?.User?.FindFirst("schema")?.Value;
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

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .ReplaceService<IModelCacheKeyFactory, DbSchemaModelCacheKeyFactory>()
                .ReplaceService<IMigrationsAssembly, DbSchemaMigrationAssembly>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (!string.IsNullOrEmpty(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }

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

    public interface IDbContextSchema
    {
        string Schema { get; }
    }

    public class DbSchemaModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
        {
            return new SchemaModelCacheKey(context, context is IDbContextSchema schema ? schema.Schema : null);
        }
    }

    public class SchemaModelCacheKey : ModelCacheKey
    {
        private readonly string _schema;

        public SchemaModelCacheKey(DbContext context, string schema) : base(context)
        {
            _schema = schema;
        }

        public override int GetHashCode() => string.IsNullOrEmpty(_schema) ? base.GetHashCode() : _schema.GetHashCode();
    }

    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
    public class DbSchemaMigrationAssembly : MigrationsAssembly
    {
        private readonly DbContext _context;
        public DbSchemaMigrationAssembly(
              ICurrentDbContext currentContext
            , IDbContextOptions options
            , IMigrationsIdGenerator idGenerator
            , IDiagnosticsLogger<DbLoggerCategory.Migrations> logger) : base(currentContext, options, idGenerator, logger)
        {
            _context = currentContext.Context;
        }

        public override Migration CreateMigration(TypeInfo migrationClass, string activeProvider)
        {
            if (activeProvider == null)
                throw new ArgumentNullException(nameof(activeProvider));

            var hasCtorWithSchema = migrationClass.GetConstructor(new[] { typeof(IDbContextSchema) }) != null;

            if (hasCtorWithSchema && _context is IDbContextSchema schema)
            {
                var instance = (Migration)Activator.CreateInstance(migrationClass.AsType(), schema);
                instance.ActiveProvider = activeProvider;
                return instance;
            }

            return base.CreateMigration(migrationClass, activeProvider);
        }
    }
}
