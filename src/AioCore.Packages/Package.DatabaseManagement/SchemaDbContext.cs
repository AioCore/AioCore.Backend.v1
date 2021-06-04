using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Package.DatabaseManagement.Infrastructure;

namespace Package.DatabaseManagement
{
    public abstract class SchemaDbContext : DbContext, ISchemaDbContext
    {
        public string Schema { get; set; }

        protected SchemaDbContext(DbContextOptions options, ISchemaDbContext schema) : base(options)
        {
            Schema = schema?.Schema;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .ReplaceService<IModelCacheKeyFactory, DbSchemaAwareModelCacheKeyFactory>()
                .ReplaceService<IMigrationsAssembly, DbSchemaAwareMigrationAssembly>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (!string.IsNullOrEmpty(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }
        }
    }
}
