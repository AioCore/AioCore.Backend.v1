using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using MediatR;
using Package.Elasticsearch;
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
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _elasticsearchService.GetAsync<SecurityUser>(request.Id);
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