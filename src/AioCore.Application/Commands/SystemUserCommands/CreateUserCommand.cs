using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemUserResponses;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;
using Package.Extensions;

namespace AioCore.Application.Commands.SystemUserCommands
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        internal class Handler : IRequestHandler<CreateUserCommand, CreateUserResponse>
        {
            private readonly IElasticsearchService _elasticsearchService;
            private readonly IAioCoreUnitOfWork _context;

            public Handler(
                IElasticsearchService elasticsearchService,
                IAioCoreUnitOfWork context)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
                _context = context;
            }

            public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var item = _context.SystemUsers.Add(new SystemUser
                {
                    Name = request.Name,
                    Account = request.Email,
                    Email = request.Email,
                    PasswordHash = request.Password.CreateMd5(),
                });
                await _context.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item.MapTo<CreateUserResponse>();
            }
        }
    }
}