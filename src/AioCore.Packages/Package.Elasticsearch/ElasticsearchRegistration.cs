using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace Package.Elasticsearch
{
    public static class ElasticsearchRegistration
    {
        public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticSearchConfig = new ElasticsearchConfig();
            configuration.Bind("Elasticsearch", elasticSearchConfig);

            var settings = new ConnectionSettings(new Uri(elasticSearchConfig.Url))
                .DefaultIndex(configuration["Elasticsearch:Index"]);

            if (!string.IsNullOrEmpty(elasticSearchConfig.UserName))
            {
                settings.BasicAuthentication(elasticSearchConfig.UserName, elasticSearchConfig.Password);
                settings.ServerCertificateValidationCallback((_, _, _, _) => true);
            }

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            return services;
        }
    }
}