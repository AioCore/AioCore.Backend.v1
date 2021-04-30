using AioCore.Application;
using AioCore.Application.AutofacModules;
using AioCore.Application.IntegrationEvents;
using AioCore.Infrastructure;
using AioCore.Shared;
using AioCore.Shared.Filters;
using Autofac;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Package.EventBus.EventBus;
using Package.EventBus.EventBus.Abstractions;
using Package.EventBus.EventBus.RabbitMQ;
using Package.EventBus.EventBus.ServiceBus;
using Package.EventBus.IntegrationEventLogEF;
using Package.EventBus.IntegrationEventLogEF.Services;
using RabbitMQ.Client;
using System;
using System.Data.Common;
using System.Reflection;

namespace AioCore.API
{
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationHelper).GetTypeInfo().Assembly);

            services.AddCustomMvc()
                .AddCustomDbContext()
                .AddCustomSwagger(_configuration)
                .AddCustomIntegrations(_configuration)
                .AddEventBus(_configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger<Startup>().LogDebug("LogDebug Starting...");

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Document API aioc.vn");
                });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        }
    }

    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddNewtonsoftJson();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {
            var configuration = AioCoreConfigs.Configuration();

            services.AddDbContext<AioCoreContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionString"], b =>
                {
                    b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    b.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                });
            });

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionString"],
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    });
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<EnumSchemaFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Document API aioc.vn",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                _ => c => new IntegrationEventLogService(c));

            services.AddTransient<IAioIntegrationEventService, AioIntegrationEventService>();

            if (configuration.GetValue<bool>("EventBus:AzureServiceBusEnabled"))
            {
                services.AddSingleton<IServiceBusPersisterConnection>(_ =>
                {
                    var serviceBusConnectionString = configuration["EventBus:Connection"];
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);
                    var subscriptionClientName = configuration["EventBus:SubscriptionClientName"];

                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, subscriptionClientName);
                });
            }
            else
            {
                services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = configuration["EventBus:Connection"],
                        DispatchConsumersAsync = true
                    };

                    if (!string.IsNullOrEmpty(configuration["EventBus:UserName"]))
                    {
                        factory.UserName = configuration["EventBus:UserName"];
                    }

                    if (!string.IsNullOrEmpty(configuration["EventBus:Password"]))
                    {
                        factory.Password = configuration["EventBus:Password"];
                    }

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBus:RetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBus:RetryCount"]);
                    }

                    return new DefaultRabbitMqPersistentConnection(factory, logger, retryCount);
                });
            }

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("EventBus:AzureServiceBusEnabled"))
            {
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, iLifetimeScope);
                });
            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
                {
                    var subscriptionClientName = configuration["EventBus:SubscriptionClientName"];
                    var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBus:RetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBus:RetryCount"]);
                    }

                    return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}