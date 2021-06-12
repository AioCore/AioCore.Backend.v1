﻿using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.DynamicAggregatesModel;
using AioCore.Shared.Common;
using AioCore.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Package.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.DynamicEntityCommand
{
    public class UpdateEntityCommand : IRequest<UpdateEntityRespone>
    {
        public Guid EntityId { get; set; }
        public string Name { get; set; }
        public List<AttributeValueModel> AttributeValues { get; set; }

        internal class Handler : IRequestHandler<UpdateEntityCommand, UpdateEntityRespone>
        {
            private readonly ITenantService _tenantService;
            private readonly IAioDynamicUnitOfWork _dynamicUnitOfWork;

            public Handler(
                  ITenantService tenantService
                , IAioDynamicUnitOfWork dynamicUnitOfWork)
            {
                _tenantService = tenantService;
                _dynamicUnitOfWork = dynamicUnitOfWork;
            }

            public async Task<UpdateEntityRespone> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
            {
                var currentTenantId = _tenantService.GetCurrentTenant();
                var dynamicEntity = await _dynamicUnitOfWork
                    .DynamicEntities
                        .Include(t => t.DynamicDateValues)
                        .Include(t => t.DynamicFloatValues)
                        .Include(t => t.DynamicGuidValues)
                        .Include(t => t.DynamicIntegerValues)
                        .Include(t => t.DynamicStringValues)
                    .FirstOrDefaultAsync(t => t.Id == request.EntityId, cancellationToken);
                if (dynamicEntity is null)
                {
                    throw new AioCoreException("Current entity does not exists");
                }
                if (dynamicEntity.TenantId != currentTenantId)
                {
                    throw new AioCoreException("Current entity does not exists for this tenant");
                }

                var attributes = await _dynamicUnitOfWork.DynamicAttributes
                        .Where(t => t.EntityTypeId == dynamicEntity.EntityTypeId)
                        .ToListAsync(cancellationToken);

                void Update<T, TType>(ICollection<T> dynamicValues, DataType dataType) where T : DynamicValue<TType>
                {
                    if (dynamicValues?.Any() != true) return;
                    var attrValues = attributes.Select(t =>
                    {
                        var attrVal = request.AttributeValues?.FirstOrDefault(x => t.DataType == dataType.ToString() && x.Name == t.Name);
                        if (attrVal is null) return null;
                        return new { t.Id, attrVal.Value };
                    })
                    .Where(t => t != null)
                    .ToList();

                    foreach (var dynamicValue in dynamicValues)
                    {
                        var attrVal = attrValues.FirstOrDefault(t => t.Id == dynamicValue.AttributeId);
                        if (attrVal is null) continue;
                        if (attrVal.Value.TryConvertTo<TType>(out var val))
                        {
                            dynamicValue.Value = val;
                        }
                    }
                }

                dynamicEntity.Name = request.Name;
                Update<DynamicDateValue, DateTimeOffset>(dynamicEntity?.DynamicDateValues, DataType.DateTime);
                Update<DynamicIntegerValue, int>(dynamicEntity?.DynamicIntegerValues, DataType.Number);
                Update<DynamicFloatValue, float>(dynamicEntity?.DynamicFloatValues, DataType.Float);
                Update<DynamicGuidValue, Guid>(dynamicEntity?.DynamicGuidValues, DataType.Guid);
                Update<DynamicStringValue, string>(dynamicEntity?.DynamicStringValues, DataType.Text);

                await _dynamicUnitOfWork.SaveChangesAsync(cancellationToken);

                return new UpdateEntityRespone
                {
                    Success = true
                };
            }
        }

    }
}
