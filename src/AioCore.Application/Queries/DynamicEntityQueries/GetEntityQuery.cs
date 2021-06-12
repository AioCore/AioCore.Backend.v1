using MediatR;
using Package.Elasticsearch;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.DynamicEntityQueries
{
    public class GetEntityQuery : IRequest<object>
    {
        public string Id { get; set; }

        class Handler : IRequestHandler<GetEntityQuery, object>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService;
            }

            public async Task<object> Handle(GetEntityQuery request, CancellationToken cancellationToken)
            {
                return await _elasticsearchService.GetAsync<dynamic>(request.Id);
            }
        }
    }
}
