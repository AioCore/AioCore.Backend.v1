using AioCore.Infrastructure.DbContexts;
using AioCore.Shared.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Package.EventBus.IntegrationEventLogEF;
using Serilog;
using Serilog.Context;
using System;

namespace AioCore.API
{
    public static class Program
    {
        private static readonly string Namespace = typeof(Program).Namespace;
        private static readonly string AppName = Namespace;
        private static IConfiguration configuration;

        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                Log.Logger = CreateSerilogLogger(configuration);

                Log.Information("Applying migrations ({ApplicationContext})...", AppName);
                host.MigrateDbContext<AioCoreContext>((context, services) =>
                {
                    var logger = services.GetRequiredService<ILogger<AioCoreContextSeed>>();

                    new AioCoreContextSeed()
                        .SeedAsync(context, logger)
                        .Wait();
                }).MigrateDbContext<IntegrationEventLogContext>((_, __) => { }); ;

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hostingContext, configBuilder) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    Console.WriteLine($"Hosting Environment: {env.EnvironmentName}");
                    configBuilder.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);

                    configuration = configBuilder.Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .CaptureStartupErrors(false)
                        .UseStartup<Startup>();
                });

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Information()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger()
                .ForContext(LogContext.Clone());
        }
    }
}