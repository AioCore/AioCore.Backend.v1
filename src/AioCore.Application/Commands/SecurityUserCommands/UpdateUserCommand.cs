using AioCore.Application.Responses.SecurityUserResponses;
using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using AioCore.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using Package.Elasticsearch;
using Package.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SecurityUserCommands
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        internal class Handler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
        {
            private readonly ISecurityUserRepository _securityUserRepository;
            private readonly IElasticsearchService _elasticsearchService;
            private readonly IStringLocalizer<Localization> _localizer;

            public Handler(
                ISecurityUserRepository securityUserRepository,
                IElasticsearchService elasticsearchService,
                IStringLocalizer<Localization> localizer)
            {
                _securityUserRepository = securityUserRepository ?? throw new ArgumentNullException(nameof(securityUserRepository));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
                _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            }

            public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _securityUserRepository.GetAsync(request.Id);
                user.Update(request.Name, request.Email);
                _securityUserRepository.Update(user);
                var res = await _securityUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.UpdateAsync(user);
                var message = res.Equals(1)
                    ? _localizer[Message.SecurityUserUpdateMessageSuccess]
                    : _localizer[Message.SecurityUserUpdateMessageFail];
                return new UpdateUserResponse { Message = message };
            }
        }
    }
}