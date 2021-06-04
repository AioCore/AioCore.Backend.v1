using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Domain.SystemAggregatesModel.SystemUserAggregate;
using MediatR;
using Package.Elasticsearch;

namespace AioCore.Application.Queries.SystemTenantQueries
{
    public class ListTenantUsersQuery : IRequest<List<SystemUser>>
    {
        public Guid TenantId { get; set; }

        public string Keyword { get; set; }

        internal class Handler : IRequestHandler<ListTenantUsersQuery, List<SystemUser>>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<List<SystemUser>> Handle(ListTenantUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _elasticsearchService.SearchAsync<SystemUser>(request.Keyword, new List<QueryAdvanced>
                {
                    new (nameof(request.TenantId), Function.Equal, DataType.Guid, request.TenantId.ToString())
                });
                return users;
            }
        }

        public void MergeParams(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}