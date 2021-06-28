using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemUserResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Shared;
using Microsoft.Extensions.Localization;
using Package.Elasticsearch;
using Package.Localization;

namespace AioCore.Application.Commands.SystemUserCommands
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        internal class Handler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
        {
            private readonly IAioCoreUnitOfWork _context;
            private readonly IElasticsearchService _elasticsearchService;
            private readonly IStringLocalizer<Localization> _localizer;

            public Handler(
                IAioCoreUnitOfWork context,
                IElasticsearchService elasticsearchService,
                IStringLocalizer<Localization> localizer)
            {
                _context = context;
                _elasticsearchService = elasticsearchService;
                _localizer = localizer;
            }

            public async Task<Response<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.SystemUsers.FindAsync(new object[] { request.Id }, cancellationToken);
                user.Name = request.Name;
                user.Email = request.Email;
                _context.SystemUsers.Update(user);
                var res = await _context.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.UpdateAsync(user);

                if (res.Equals(1))
                {
                    return new UpdateUserResponse
                    {
                        Message = _localizer[Message.SystemUserUpdateMessageSuccess]
                    };
                }

                return new Response<UpdateUserResponse>
                {
                    Success = false,
                    Status = HttpStatusCode.BadRequest,
                    Data = new UpdateUserResponse
                    {
                        Message = _localizer[Message.SystemUserUpdateMessageFail]
                    }
                };
            }
        }
    }
}