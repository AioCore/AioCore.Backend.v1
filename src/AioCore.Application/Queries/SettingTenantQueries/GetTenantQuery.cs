using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using MediatR;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SettingTenantQueries
{
    public class GetTenantQuery : IRequest<GetTenantResponse>
    {
        public Guid Id { get; private set; }

        public void MergeParams(Guid id)
        {
            Id = id;
        }

        internal class Handler : IRequestHandler<GetTenantQuery, GetTenantResponse>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<GetTenantResponse> Handle(GetTenantQuery request, CancellationToken cancellationToken)
            {
                var tenant = await _elasticsearchService.GetAsync<SettingTenant>(request.Id);
                return new GetTenantResponse(tenant.Id, tenant.Name);
            }
        }
    }

    public record GetTenantResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public GetTenantResponse(
            Guid id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}