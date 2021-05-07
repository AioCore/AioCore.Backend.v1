using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using MediatR;
using Package.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SettingTenantQueries
{
    public class ListTenantUsersQuery : IRequest<List<SecurityUser>>
    {
        public Guid TenantId { get; set; }

        public string Keyword { get; set; }

        internal class Handler : IRequestHandler<ListTenantUsersQuery, List<SecurityUser>>
        {
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(IElasticsearchService elasticsearchService)
            {
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<List<SecurityUser>> Handle(ListTenantUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _elasticsearchService.SearchAsync<SecurityUser>(request.Keyword, new List<QueryAdvanced>
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