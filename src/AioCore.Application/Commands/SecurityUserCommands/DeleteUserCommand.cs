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
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }

        internal class Handler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
        {
            private readonly ISecurityUserRepository _securityUserRepository;
            private readonly IStringLocalizer<Localization> _localizer;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                ISecurityUserRepository securityUserRepository,
                IStringLocalizer<Localization> localizer,
                IElasticsearchService elasticsearchService)
            {
                _securityUserRepository = securityUserRepository ?? throw new ArgumentNullException(nameof(securityUserRepository));
                _localizer = localizer ?? throw new ArgumentNullException();
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                _securityUserRepository.Delete(request.Id);
                var res = await _securityUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.DeleteAsync<SecurityUser>(request.Id);
                return new DeleteUserResponse
                {
                    Message = res.Equals(1)
                        ? _localizer[Message.SecurityUserDeleteMessageSuccess]
                        : _localizer[Message.SecurityUserDeleteMessageFail]
                };
            }
        }
    }
}