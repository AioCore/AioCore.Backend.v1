using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using MediatR;
using Package.Elasticsearch;

namespace AioCore.Application.Queries.SystemUserQueries
{
    public class ListUserQuery : IRequest<Pagination<SystemUser>>
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

        internal class Handler : IRequestHandler<ListUserQuery, Pagination<SystemUser>>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService;
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<Pagination<SystemUser>> Handle(ListUserQuery request, CancellationToken cancellationToken)
            {
                return await _elasticsearchService.SearchAsync<SystemUser>(request.PageIndex, request.PageSize, request.Keyword);
            }
        }
    }
}