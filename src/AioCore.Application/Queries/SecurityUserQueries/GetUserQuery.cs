using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SecurityUserQueries
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public Guid Id { get; private set; }

        public void MergeParams(Guid id)
        {
            Id = id;
        }

        internal class Handler : IRequestHandler<GetUserQuery, GetUserResponse>
        {
            private readonly ISecurityUserRepository _securityUserRepository;

            public Handler(ISecurityUserRepository securityUserRepository)
            {
                _securityUserRepository = securityUserRepository ?? throw new ArgumentNullException(nameof(securityUserRepository));
            }

            public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _securityUserRepository.GetAsync(request.Id);
                return new GetUserResponse(user.Id, user.Name, user.Email);
            }
        }
    }

    public record GetUserResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public GetUserResponse(
            Guid id,
            string name,
            string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}