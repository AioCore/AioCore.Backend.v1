using AioCore.Application.Responses.DynamicBinaryResponses;
using AioCore.Domain.SystemAggregatesModel.SystemBinaryAggregate;
using MediatR;
using Package.AutoMapper;
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
            private readonly ISystemBinaryRepository _systemBinaryRepository;

            public Handler(
                IFileServerService fileServerService,
                ISystemBinaryRepository systemBinaryRepository)
            {
                _fileServerService = fileServerService ?? throw new ArgumentNullException(nameof(fileServerService));
                _systemBinaryRepository = systemBinaryRepository ?? throw new ArgumentNullException(nameof(systemBinaryRepository));
            }

            public async Task<GetBinaryResponse> Handle(GetBinaryQuery request, CancellationToken cancellationToken)
            {
                var binary = await _systemBinaryRepository.GetAsync(request.Id);
                var bytes = await _fileServerService.DownloadFileByteAsync(binary.FilePath);
                var res = binary.To<GetBinaryResponse>();
                res.MergeParams(bytes);
                return res;
            }
        }
    }
}