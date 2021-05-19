using AioCore.Domain.AggregatesModel.SettingLayoutAggregate;
using MediatR;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SettingLayoutQueries
{
    public class ListLayoutQuery : IRequest<Pagination<SettingLayout>>
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

        internal class Handler : IRequestHandler<ListLayoutQuery, Pagination<SettingLayout>>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<Pagination<SettingLayout>> Handle(ListLayoutQuery request, CancellationToken cancellationToken)
            {
                return await _elasticsearchService.SearchAsync<SettingLayout>(request.PageIndex, request.PageSize, request.Keyword);
            }
        }
    }
}