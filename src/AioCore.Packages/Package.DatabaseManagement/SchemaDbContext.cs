using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Package.DatabaseManagement.Infrastructure;
using System;

namespace Package.DatabaseManagement
{
    public abstract class SchemaDbContext : DbContext, ISchemaDbContext
    {
        public string Schema { get; set; }

        protected SchemaDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            Schema = serviceProvider?.GetService<ISchemaDbContext>()?.Schema;
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
