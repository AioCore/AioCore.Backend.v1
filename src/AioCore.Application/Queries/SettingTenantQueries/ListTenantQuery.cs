using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using MediatR;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SettingTenantQueries
{
    public class ListTenantQuery : IRequest<Pagination<SettingTenant>>
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

        internal class Handler : IRequestHandler<ListTenantQuery, Pagination<SettingTenant>>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService;
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<Pagination<SettingTenant>> Handle(ListTenantQuery request, CancellationToken cancellationToken)
            {
                return await _elasticsearchService.SearchAsync<SettingTenant>(request.PageIndex, request.PageSize, request.Keyword);
            }
        }
    }
}