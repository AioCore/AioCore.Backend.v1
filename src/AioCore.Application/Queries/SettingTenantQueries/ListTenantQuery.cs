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
        public int PageSize { get; private set; }

        public int PageIndex { get; private set; }

        public string Keyword { get; private set; }

        public void MergeParams(int pageSize, int pageIndex, string keyword)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Keyword = keyword;
        }

        internal class Handler : IRequestHandler<ListTenantQuery, ListTenantResponse>
        {
            private readonly ISettingTenantRepository _settingTenantRepository;

            public Handler(ISettingTenantRepository settingTenantRepository)
            {
                _settingTenantRepository = settingTenantRepository ?? throw new ArgumentNullException(nameof(settingTenantRepository));
            }

            public async Task<ListTenantResponse> Handle(ListTenantQuery request, CancellationToken cancellationToken)
            {
                var res = await _settingTenantRepository.GetAsync(request.PageSize * (request.PageIndex - 1),
                        request.PageSize, request.Keyword)
                    .Select(x => new GetTenantResponse(x.Id, x.Name)).ToListAsync(cancellationToken);
                var count = await _settingTenantRepository.LongCountAsync();
                return new ListTenantResponse(request.PageSize, request.PageIndex, count, res);
            }
        }
    }

    public record ListTenantResponse
    {
        public int PageSize { get; private set; }

        public int PageIndex { get; private set; }

        public long Count { get; private set; }

        public List<GetTenantResponse> Items { get; private set; }

        public ListTenantResponse(
            int pageSize,
            int pageIndex,
            long count,
            List<GetTenantResponse> items)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Items = items;
        }
    }
}