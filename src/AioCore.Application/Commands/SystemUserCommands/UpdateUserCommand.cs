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
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        internal class Handler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
        {
            private readonly ISystemUserRepository _systemUserRepository;
            private readonly IElasticsearchService _elasticsearchService;
            private readonly IStringLocalizer<Localization> _localizer;

            public Handler(
                ISystemUserRepository systemUserRepository,
                IElasticsearchService elasticsearchService,
                IStringLocalizer<Localization> localizer)
            {
                _systemUserRepository = systemUserRepository ?? throw new ArgumentNullException(nameof(systemUserRepository));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
                _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            }

            public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _systemUserRepository.GetAsync(request.Id);
                user.Update(request.Name, request.Email);
                _systemUserRepository.Update(user);
                var res = await _systemUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.UpdateAsync(user);
                var message = res.Equals(1)
                    ? _localizer[Message.SystemUserUpdateMessageSuccess]
                    : _localizer[Message.SystemUserUpdateMessageFail];
                return new UpdateUserResponse { Message = message };
            }
        }
    }
}