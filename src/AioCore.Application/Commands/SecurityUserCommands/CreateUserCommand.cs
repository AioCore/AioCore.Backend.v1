using AioCore.Application.Responses.SecurityUserResponses;
using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SecurityUserCommands
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
            private readonly ISecurityUserRepository _securityUserRepository;

            public Handler(
                IElasticsearchService elasticsearchService, ISecurityUserRepository securityUserRepository)
            {
                _securityUserRepository = securityUserRepository ?? throw new ArgumentNullException(nameof(securityUserRepository));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var item = _securityUserRepository.Add(new SecurityUser(request.Name, request.Email, request.TenantId, request.Password));
                await _securityUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item.To<CreateUserResponse>();
            }
        }
    }
}