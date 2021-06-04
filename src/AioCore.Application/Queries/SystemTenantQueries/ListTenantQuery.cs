using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using MediatR;
using Package.Elasticsearch;

namespace AioCore.Application.Queries.SystemTenantQueries
{
    public class ListTenantQuery : IRequest<Pagination<SystemTenant>>
    {
        public int PageSize { get; private set; }

        public int PageIndex { get; private set; }

        public string Keyword { get; private set; }

        public void MergeParams(int pageSize, int pageIndex, string keyword)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Keyword = keyword;
        }

        internal class Handler : IRequestHandler<ListTenantQuery, Pagination<SystemTenant>>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<Pagination<SystemTenant>> Handle(ListTenantQuery request, CancellationToken cancellationToken)
            {
                return await _elasticsearchService.SearchAsync<SystemTenant>(request.PageIndex, request.PageSize, request.Keyword);
            }
        }
    }
}