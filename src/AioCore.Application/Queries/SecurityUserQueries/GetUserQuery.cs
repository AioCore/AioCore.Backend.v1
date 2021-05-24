using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.AggregatesModel.SystemUserAggregate;
using MediatR;

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
            private readonly ISystemUserRepository _systemUserRepository;

            public Handler(ISystemUserRepository systemUserRepository)
            {
                _systemUserRepository = systemUserRepository ?? throw new ArgumentNullException(nameof(systemUserRepository));
            }

            public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _systemUserRepository.GetAsync(request.Id);
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