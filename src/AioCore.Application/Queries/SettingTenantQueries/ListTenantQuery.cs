using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Queries.SettingTenantQueries
{
    public class ListTenantQuery : IRequest<ListTenantResponse>
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public string Query { get; set; }

        internal class Handler : IRequestHandler<ListTenantQuery, ListTenantResponse>
        {
            private readonly ISettingTenantRepository _settingTenantRepository;

            public Handler(ISettingTenantRepository settingTenantRepository)
            {
                _settingTenantRepository = settingTenantRepository ?? throw new ArgumentNullException(nameof(settingTenantRepository));
            }

            public async Task<ListTenantResponse> Handle(ListTenantQuery request, CancellationToken cancellationToken)
            {
                var res = await _settingTenantRepository.GetAsync(request.Skip, request.Take, request.Query)
                    .Select(x => new GetTenantResponse(x.Id, x.Name)).ToListAsync(cancellationToken);
                return new ListTenantResponse(res);
            }
        }
    }

    public record ListTenantResponse
    {
        public List<GetTenantResponse> Items { get; set; }

        public ListTenantResponse(List<GetTenantResponse> items)
        {
            Items = items;
        }
    }
}