using AioCore.Application.Responses.DynamicBinaryResponses;
using MediatR;
using Package.AutoMapper;
using Package.FileServer;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;

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
            private readonly IAioCoreUnitOfWork _context;

            public Handler(
                IFileServerService fileServerService,
                IAioCoreUnitOfWork context)
            {
                _fileServerService = fileServerService ?? throw new ArgumentNullException(nameof(fileServerService));
                _context = context;
            }

            public async Task<GetBinaryResponse> Handle(GetBinaryQuery request, CancellationToken cancellationToken)
            {
                var binary = await _context.SystemBinaries.FindAsync(request.Id, cancellationToken);
                var bytes = await _fileServerService.DownloadFileByteAsync(binary.FilePath);
                var res = binary.MapTo<GetBinaryResponse>();
                res.MergeParams(bytes);
                return res;
            }
        }
    }
}