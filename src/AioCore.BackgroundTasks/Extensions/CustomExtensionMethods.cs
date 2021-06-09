using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Package.EventBus.EventBus;
using Package.EventBus.EventBus.Abstractions;
using Package.EventBus.EventBus.RabbitMQ;
using Package.EventBus.EventBus.ServiceBus;
using RabbitMQ.Client;
using Serilog;

namespace AioCore.BackgroundTasks.Extensions
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                services.AddSingleton<IServiceBusPersisterConnection>(_ =>
                {
                    var serviceBusConnectionString = configuration["EventBusConnection"];
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, subscriptionClientName);
                });

                services.AddSingleton<IEventBus, EventBusServiceBus>();
            }
            else
            {
                services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = configuration["EventBusConnection"],
                        DispatchConsumersAsync = true
                    };

                    if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                    {
                        factory.UserName = configuration["EventBusUserName"];
                    }

                    if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                    {
                        factory.Password = configuration["EventBusPassword"];
                    }

                    var retryCount = 5;

                    if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBusRetryCount"]);
                    }

                    return new DefaultRabbitMqPersistentConnection(factory, logger, retryCount);
                });

                services.AddSingleton<IEventBus, EventBusRabbitMq>();
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static ILoggingBuilder UseSerilog(this ILoggingBuilder builder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", Program.AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return builder;
        }
    }
}