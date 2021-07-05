using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemUserResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.CoreEntities;
using AioCore.Mediator;
using AioCore.Shared;
using Microsoft.Extensions.Localization;
using Package.Elasticsearch;
using Package.Localization;

namespace AioCore.Application.Commands.SystemUserCommands
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }

        internal class Handler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
        {
            private readonly IAioCoreUnitOfWork _context;
            private readonly IStringLocalizer<Localization> _localizer;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                IAioCoreUnitOfWork context,
                IStringLocalizer<Localization> localizer,
                IElasticsearchService elasticsearchService)
            {
                _context = context;
                _localizer = localizer;
                _elasticsearchService = elasticsearchService;
            }

            public async Task<Response<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                _context.SystemUsers.Delete(request.Id);
                var res = await _context.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.DeleteAsync<SystemUser>(request.Id);
                if (res.Equals(1))
                {
                    return new DeleteUserResponse
                    {
                        Message = _localizer[Message.SystemUserDeleteMessageSuccess]
                    };
                }

                return new Response<DeleteUserResponse>
                {
                    Success = false,
                    Status = HttpStatusCode.BadRequest,
                    Data = new DeleteUserResponse
                    {
                        Message = _localizer[Message.SystemUserDeleteMessageFail]
                    }
                };
            }
        }
    }
}