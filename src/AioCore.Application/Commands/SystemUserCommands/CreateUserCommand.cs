using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemUserResponses;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;

namespace AioCore.Application.Commands.SystemUserCommands
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Guid TenantId { get; set; }

        internal class Handler : IRequestHandler<CreateUserCommand, CreateUserResponse>
        {
            private readonly IElasticsearchService _elasticsearchService;
            private readonly ISystemUserRepository _systemUserRepository;

            public Handler(
                IElasticsearchService elasticsearchService, ISystemUserRepository systemUserRepository)
            {
                _systemUserRepository = systemUserRepository ?? throw new ArgumentNullException(nameof(systemUserRepository));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var item = _systemUserRepository.Add(new SystemUser(request.Name, request.Email, request.TenantId, request.Password));
                await _systemUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item.To<CreateUserResponse>();
            }
        }
    }
}