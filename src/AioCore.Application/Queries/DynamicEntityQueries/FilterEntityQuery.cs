using AioCore.Domain.Models;
using AioCore.Shared.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Package.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;

namespace AioCore.Application.Queries.DynamicEntityQueries
{
    public class FilterEntityQuery : IRequest<Pagination<object>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Guid FilterId { get; set; }
        public Dictionary<Guid, string> Parameters { get; set; }

        internal class Handler : IRequestHandler<FilterEntityQuery, Pagination<object>>
        {
            private readonly IElasticsearchService _elasticsearchService;
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;
            private readonly IAioDynamicUnitOfWork _dynamicUnitOfWork;

            public Handler(
                  IElasticsearchService elasticsearchService
                , IAioCoreUnitOfWork coreUnitOfWork
                , IAioDynamicUnitOfWork dynamicUnitOfWork)
            {
                _elasticsearchService = elasticsearchService;
                _coreUnitOfWork = coreUnitOfWork;
                _dynamicUnitOfWork = dynamicUnitOfWork;
            }

            public async Task<Pagination<object>> Handle(FilterEntityQuery request, CancellationToken cancellationToken)
            {
                var component = await _coreUnitOfWork.SettingComponents
                    .Where(x => x.ParentId == request.FilterId && x.ComponentType == ComponentType.Filter)
                    .FirstOrDefaultAsync(cancellationToken);

                if (component is null)
                {
                    return new Pagination<object>(request.Page, request.PageSize, null, 0);
                }

                var filterSettings = component.GetComponentSettings<FilterSettings>();

                if (filterSettings is null)
                {
                    return new Pagination<object>(request.Page, request.PageSize, null, 0);
                }

                var attributes = await _dynamicUnitOfWork.DynamicAttributes
                    .Where(t => t.EntityTypeId == filterSettings.EntityTypeId)
                    .ToListAsync(cancellationToken);

                var parameters = request.Parameters.Select(t =>
                {
                    var attr = attributes.FirstOrDefault(x => x.Id == t.Key);
                    if (attr is null) return null;
                    var setting = filterSettings.Parameters?.FirstOrDefault(x => x.AttributeId == t.Key);
                    if (setting is null) return null;

                    return new QueryAdvanced(attr.Name, setting.Function, attr.DataType, t.Value, setting.Operator);
                })
                .Where(t => t is not null)
                .ToList();

                return await _elasticsearchService.SearchDynamicAsync(filterSettings.EntityTypeId, request.Page, request.PageSize, parameters);
            }
        }
    }
}