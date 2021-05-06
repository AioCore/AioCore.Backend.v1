using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using MediatR;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SecurityUserQueries
{
    public class ListUserQuery : IRequest<Pagination<SecurityUser>>
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

        internal class Handler : IRequestHandler<ListUserQuery, Pagination<SecurityUser>>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService;
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<Pagination<SecurityUser>> Handle(ListUserQuery request, CancellationToken cancellationToken)
            {
                return await _elasticsearchService.SearchAsync<SecurityUser>(request.PageIndex, request.PageSize, request.Keyword);
            }
        }
    }
}