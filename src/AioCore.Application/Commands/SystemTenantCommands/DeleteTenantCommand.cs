using AioCore.Domain.CoreEntities;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;
using AioCore.Mediator;
using Microsoft.Extensions.Logging;
using Package.Elasticsearch;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SystemTenantCommands
{
    public class DeleteTenantCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        internal class Handler : IRequestHandler<DeleteTenantCommand, Guid>
        {
            private readonly IAioCoreUnitOfWork _context;
            private readonly IElasticsearchService _elasticsearchService;
            private readonly ILogger<Handler> _logger;

            public Handler(
                IAioCoreUnitOfWork context,
                IElasticsearchService elasticsearchService,
                ILogger<Handler> logger)
            {
                _context = context;
                _elasticsearchService = elasticsearchService;
                _logger = logger;
            }

            public async Task<Response<Guid>> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await _context.SystemTenants.DeleteAsync(request.Id, cancellationToken);
                    await _elasticsearchService.DeleteAsync<SystemTenant>(request.Id);
                    await _context.SaveChangesAsync(cancellationToken);
                    return new Response<Guid>
                    {
                        Message = "Xóa thuê bao thành công",
                        Status = HttpStatusCode.OK,
                        Success = true,
                        Data = request.Id
                    };
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return new Response<Guid>
                    {
                        Message = "Xóa thuê bao không thành công",
                        Status = HttpStatusCode.NotFound,
                        Success = false,
                        Data = request.Id
                    };
                }
            }
        }
    }
}