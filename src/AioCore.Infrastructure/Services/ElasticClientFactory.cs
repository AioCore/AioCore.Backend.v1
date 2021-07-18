using Microsoft.Extensions.Configuration;
using Nest;
using Package.Elasticsearch;
using System;
using System.Collections.Concurrent;
using AioCore.Application.Repositories;
using AioCore.Application.Services;

namespace AioCore.Infrastructure.Services
{
    public class ElasticClientFactory : IElasticClientFactory
    {
        private readonly ITenantService _tenantService;
        private readonly ISettingTenantRepository _tenantRepository;
        private readonly IConfiguration _configuration;

        public ElasticClientFactory(
              ITenantService tenantService
            , ISettingTenantRepository tenantRepository
            , IConfiguration configuration)
        {
            _tenantService = tenantService;
            _tenantRepository = tenantRepository;
            _configuration = configuration;
        }

        private static readonly ConcurrentDictionary<string, ElasticClient> _esClients = new();

        public IElasticClient CreateElasticClient()
        {
            var currentTenantId = _tenantService.GetCurrentTenantId();
            var tenant = currentTenantId is null ? null : _tenantRepository.GetAsync(currentTenantId.Value).GetAwaiter().GetResult();
            var esInfo = ElasticsearchInfo.Parse(tenant?.ElasticsearchSettingsJson) ?? new ElasticsearchInfo
            {
                UserName = _configuration["Elasticsearch:UserName"],
                Password = _configuration["Elasticsearch:Password"],
                Url = _configuration["Elasticsearch:Url"],
                Index = _configuration["Elasticsearch:Index"]
            };
            var key = $"{esInfo.Url}_{esInfo.Index}_{esInfo.UserName}_{esInfo.Password}";

            return _esClients.GetOrAdd(key, (_) =>
            {
                var settings = new ConnectionSettings(new Uri(esInfo.Url))
                    .DefaultIndex(esInfo.Index);

                if (!string.IsNullOrEmpty(esInfo.UserName))
                {
                    settings.BasicAuthentication(esInfo.UserName, esInfo.Password);
                    settings.ServerCertificateValidationCallback((_, _, _, _) => true);
                }
                return new ElasticClient(settings);
            });
        }
    }
}