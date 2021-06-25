using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Package.DatabaseManagement
{
    public static class SchemaDbContextExtensions
    {
        public static IServiceCollection AddSchemaDbContext<TContext>(this IServiceCollection services
            , Func<IServiceProvider, DatabaseInfo> dbInfoFactory
            , ServiceLifetime contextLifetime = ServiceLifetime.Scoped
            , ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : SchemaDbContext
        {
            services.AddDbContext<TContext>((serviceProvider, options) =>
            {
                options.UseSql(dbInfoFactory(serviceProvider));
            }, contextLifetime, optionsLifetime);

            return services;
        }

        private static DbContextOptionsBuilder UseSql(this DbContextOptionsBuilder optionsBuilder, DatabaseInfo dbInfo)
        {
            var connectionString = GetConnectionString();
            var migrationsAssembly = "DynamicMigrations." + dbInfo.DatabaseType.ToString();

            return dbInfo.DatabaseType switch
            {
                DatabaseType.PostgresSql => optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                    sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbInfo.Schema);
                }),
                _ => optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                    sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbInfo.Schema);
                }),
            };

            string GetConnectionString()
            {
                if (dbInfo == null) throw new ArgumentNullException(nameof(dbInfo));

                return dbInfo.DatabaseType switch
                {
                    DatabaseType.PostgresSql => $"Host={dbInfo.Server};User ID={dbInfo.User};Password={dbInfo.Password};Database={dbInfo.Database};Pooling=true;",
                    _ => $"Server={dbInfo.Server};User ID={dbInfo.User};Password={dbInfo.Password};Database={dbInfo.Database};Pooling=true;",
                };
            }
        }
    }
}
