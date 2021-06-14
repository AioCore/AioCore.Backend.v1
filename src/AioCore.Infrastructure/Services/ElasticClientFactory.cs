using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Nest;
using Package.Elasticsearch;
using System;
using System.Collections.Concurrent;

namespace AioCore.Infrastructure.Services
{
    public class ElasticClientFactory : IElasticClientFactory
    {
        private readonly ITenantService _tenantService;
        private readonly IAioCoreUnitOfWork _coreUnitOfWork;
        private readonly IConfiguration _configuration;

        public ElasticClientFactory(
              ITenantService tenantService
            , IAioCoreUnitOfWork coreUnitOfWork
            , IConfiguration configuration)
        {
            _tenantService = tenantService;
            _coreUnitOfWork = coreUnitOfWork;
            _configuration = configuration;
        }

        private static readonly ConcurrentDictionary<string, ElasticClient> _esClients = new ConcurrentDictionary<string, ElasticClient>();

        public IElasticClient CreateElasticClient()
        {
            var currentTenantId = _tenantService.GetCurrentTenantId();

            return _esClients.GetOrAdd(currentTenantId?.ToString() ?? "default", (_) =>
            {
                var tenant = currentTenantId is null ? null : _coreUnitOfWork.SystemTenants.Find(currentTenantId);
                var esInfo = ElasticsearchInfo.Parse(tenant?.ElasticsearchInfo) ?? new ElasticsearchInfo
                {
                    UserName = _configuration["Elasticsearch:UserName"],
                    Password = _configuration["Elasticsearch:Password"],
                    Url = _configuration["Elasticsearch:Url"],
                    Index = _configuration["Elasticsearch:Index"]
                };
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
