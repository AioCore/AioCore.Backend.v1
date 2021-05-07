using AioCore.Application.Responses.DynamicBinaryResponses;
using AioCore.Domain.AggregatesModel.DynamicBinaryAggregate;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;
using Package.FileServer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.DynamicBinaryQueries
{
    public class GetBinaryQuery : IRequest<GetBinaryResponse>
    {
        public Guid Id { get; private set; }

        public void MergeParams(Guid id)
        {
            Id = id;
        }

        internal class Handler : IRequestHandler<GetBinaryQuery, GetBinaryResponse>
        {
            private readonly IFileServerService _fileServerService;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                IFileServerService fileServerService,
                IElasticsearchService elasticsearchService)
            {
                _fileServerService = fileServerService ?? throw new ArgumentNullException(nameof(fileServerService));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<GetBinaryResponse> Handle(GetBinaryQuery request, CancellationToken cancellationToken)
            {
                var binary = await _elasticsearchService.GetAsync<DynamicBinary>(request.Id);
                var bytes = await _fileServerService.DownloadFileByteAsync(binary.FilePath);
                var res = binary.To<GetBinaryResponse>();
                res.MergeParams(bytes);
                return res;
            }
        }
    }
}