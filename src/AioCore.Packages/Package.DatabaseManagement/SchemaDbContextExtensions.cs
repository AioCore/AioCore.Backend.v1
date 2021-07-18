using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Package.DatabaseManagement
{
    public static class SchemaDbContextExtensions
    {
        public static IServiceCollection AddSchemaDbContext<TContext>(this IServiceCollection services
            , Func<IServiceProvider, DatabaseSettings> dbInfoFactory
            , ServiceLifetime contextLifetime = ServiceLifetime.Scoped
            , ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : SchemaDbContext
        {
            services.AddDbContext<TContext>((serviceProvider, options) =>
            {
                options.UseSql(dbInfoFactory(serviceProvider));
            }, contextLifetime, optionsLifetime);

            return services;
        }

        private static DbContextOptionsBuilder UseSql(this DbContextOptionsBuilder optionsBuilder, DatabaseSettings dbSettings)
        {
            var connectionString = GetConnectionString();
            var migrationsAssembly = "Migration." + dbSettings.DatabaseType;

            return dbSettings.DatabaseType switch
            {
                DatabaseType.PostgresSql => optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                    sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbSettings.Schema);
                }),
                _ => optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                    sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbSettings.Schema);
                }),
            };

            string GetConnectionString()
            {
                if (dbSettings == null) throw new ArgumentNullException(nameof(dbSettings));

                return dbSettings.DatabaseType switch
                {
                    DatabaseType.PostgresSql => $"Host={dbSettings.Server};User ID={dbSettings.User};Password={dbSettings.Password};Database={dbSettings.Database};Pooling=true;",
                    _ => $"Server={dbSettings.Server};User ID={dbSettings.User};Password={dbSettings.Password};Database={dbSettings.Database};Pooling=true;",
                };
            }
        }
    }
}