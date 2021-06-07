using AioCore.Application.Responses.IdentityResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using AioCore.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using Package.Elasticsearch;
using Package.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.IdentityCommands
{
    public class SignUpCommand : IRequest<SignUpResponse>
    {
        public string Name { get; set; }

        public string Account { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        internal class Handler : IRequestHandler<SignUpCommand, SignUpResponse>
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
                _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (!request.Password.Equals(request.ConfirmPassword))
                        return new SignUpResponse { Message = _localizer[Message.SignUpMessagePasswordNotMatch] };
                    var user = await _context.SystemUsers.AddAsync(new SystemUser(request.Name, request.Account,
                        request.Email, request.Password), cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    await _elasticsearchService.IndexAsync(user);
                    return new SignUpResponse { Message = _localizer[Message.SignUpMessageSuccess] };
                }
                catch (Exception e)
                {
                    throw new ApplicationException(_localizer[Message.SignUpMessageFail, e.Message]);
                }
            }
        }
    }
}