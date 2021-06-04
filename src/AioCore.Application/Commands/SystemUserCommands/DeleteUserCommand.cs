using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemUserResponses;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using AioCore.Shared;
using MediatR;
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
            private readonly ISystemUserRepository _systemUserRepository;
            private readonly IStringLocalizer<Localization> _localizer;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                ISystemUserRepository systemUserRepository,
                IStringLocalizer<Localization> localizer,
                IElasticsearchService elasticsearchService)
            {
                _systemUserRepository = systemUserRepository ?? throw new ArgumentNullException(nameof(systemUserRepository));
                _localizer = localizer ?? throw new ArgumentNullException();
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                _systemUserRepository.Delete(request.Id);
                var res = await _systemUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.DeleteAsync<SystemUser>(request.Id);
                return new DeleteUserResponse
                {
                    Message = res.Equals(1)
                        ? _localizer[Message.SystemUserDeleteMessageSuccess]
                        : _localizer[Message.SystemUserDeleteMessageFail]
                };
            }
        }
    }
}