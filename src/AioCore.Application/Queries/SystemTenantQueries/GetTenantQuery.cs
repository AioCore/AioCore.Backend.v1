using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemTenantResponses;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using MediatR;

namespace AioCore.Application.Queries.SystemTenantQueries
{
    public class GetTenantQuery : IRequest<GetTenantResponse>
    {
        public Guid Id { get; private set; }

        public void MergeParams(Guid id)
        {
            Id = id;
        }

        internal class Handler : IRequestHandler<GetTenantQuery, GetTenantResponse>
        {
            private readonly ISettingTenantRepository _settingTenantRepository;

            public Handler(ISettingTenantRepository settingTenantRepository)
            {
                _settingTenantRepository = settingTenantRepository ?? throw new ArgumentNullException(nameof(settingTenantRepository));
            }

            public async Task<GetTenantResponse> Handle(GetTenantQuery request, CancellationToken cancellationToken)
            {
                var tenant = await _settingTenantRepository.GetAsync(request.Id);
                return new GetTenantResponse(tenant.Id, tenant.Name);
            }
        }
    }
}