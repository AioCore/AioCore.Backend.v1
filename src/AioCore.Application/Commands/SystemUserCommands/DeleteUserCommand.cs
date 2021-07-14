using AioCore.Application.Responses.SystemUserResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.CoreEntities;
using AioCore.Shared.Constants;
using Package.Elasticsearch;
using Package.Mediator;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SystemUserCommands
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }

        internal class Handler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
        {
            private readonly IAioCoreUnitOfWork _context;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                IAioCoreUnitOfWork context,
                IElasticsearchService elasticsearchService)
            {
                _context = context;
                _elasticsearchService = elasticsearchService;
            }

            public async Task<Response<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                await _context.SystemUsers.DeleteAsync(request.Id, cancellationToken);
                var res = await _context.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.DeleteAsync<SystemUser>(request.Id);
                if (res.Equals(1))
                {
                    return new DeleteUserResponse
                    {
                        Message = Messages.SystemUserDeleteSuccess
                    };
                }

                return new Response<DeleteUserResponse>
                {
                    Success = false,
                    Status = HttpStatusCode.BadRequest,
                    Data = new DeleteUserResponse
                    {
                        Message = Messages.SystemUserDeleteFail
                    }
                };
            }
        }
    }
}