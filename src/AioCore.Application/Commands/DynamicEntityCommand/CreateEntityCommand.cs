using AioCore.Application.Services;
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
    public class CreateEntityCommand : IRequest<CreateEntityRespone>
    {
        public string Name { get; set; }
        public Guid EntityTypeId { get; set; }
        public List<AttributeValueModel> AttributeValues { get; set; }

        internal class Handler : IRequestHandler<CreateEntityCommand, CreateEntityRespone>
        {
            private readonly ITenantService _tenantService;
            private readonly IAioDynamicUnitOfWork _aioDynamicUnitOfWork;
            private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;
            private readonly IDynamicEntityService _dynamicEntityService;

            public Handler(
                  ITenantService tenantService
                , IAioDynamicUnitOfWork aioDynamicUnitOfWork
                , IAioCoreUnitOfWork aioCoreUnitOfWork
                , IDynamicEntityService dynamicEntityService)
            {
                _tenantService = tenantService;
                _aioDynamicUnitOfWork = aioDynamicUnitOfWork;
                _aioCoreUnitOfWork = aioCoreUnitOfWork;
                _dynamicEntityService = dynamicEntityService;
            }

            public async Task<CreateEntityRespone> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
            {
                var currentTenantId = _tenantService.GetCurrentTenant();
                if (currentTenantId == null)
                {
                    throw new AioCoreException("Current tenant does not exists");
                }

                var entityType = await _aioCoreUnitOfWork.SettingEntityTypes.FindAsync(new object[] { request.EntityTypeId }, cancellationToken: cancellationToken);
                if (entityType == null)
                {
                    throw new AioCoreException("Current type does not exists");
                }

                if (entityType.TenantId != currentTenantId)
                {
                    throw new AioCoreException("Current type does not exists for this tenant");
                }

                var entity = await _aioDynamicUnitOfWork.DynamicEntities.AddAsync(new DynamicEntity
                {
                    Name = request.Name,
                    EntityTypeId = request.EntityTypeId,
                    TenantId = currentTenantId.Value,
                    Created = DateTime.Now
                }, cancellationToken);


                var attributes = await _aioDynamicUnitOfWork.DynamicAttributes.Where(t => t.EntityTypeId == entityType.Id)
                    .ToListAsync(cancellationToken);

                List<T> CreateDynamicValues<T, TType>(DataType dataType) where T : DynamicValue<TType>, new()
                {
                    return attributes.Where(t => t.DataType == dataType.ToString())
                        .Select(t =>
                        {
                            var value = request.AttributeValues.FirstOrDefault(x => x.Name == t.Name);
                            if (value == null) return null;
                            return CreateValue<T, TType>(entity.Id, entity.EntityTypeId, t.Id, value.Value);
                        })
                        .Where(t => t != null)
                        .ToList();
                }

                var dynamicDateValues = CreateDynamicValues<DynamicDateValue, DateTimeOffset>(DataType.DateTime);
                var dynamicIntValues = CreateDynamicValues<DynamicIntegerValue, int>(DataType.Number);
                var dynamicFloatValues = CreateDynamicValues<DynamicFloatValue, float>(DataType.Float);
                var dynamicGuidValues = CreateDynamicValues<DynamicGuidValue, Guid>(DataType.Guid);
                var dynamicStringValues = CreateDynamicValues<DynamicStringValue, string>(DataType.Text);

                await _aioDynamicUnitOfWork.DynamicDateValues.AddRangeAsync(dynamicDateValues, cancellationToken);
                await _aioDynamicUnitOfWork.DynamicFloatValues.AddRangeAsync(dynamicFloatValues, cancellationToken);
                await _aioDynamicUnitOfWork.DynamicGuidValues.AddRangeAsync(dynamicGuidValues, cancellationToken);
                await _aioDynamicUnitOfWork.DynamicIntegerValues.AddRangeAsync(dynamicIntValues, cancellationToken);
                await _aioDynamicUnitOfWork.DynamicStringValues.AddRangeAsync(dynamicStringValues, cancellationToken);

                await _aioDynamicUnitOfWork.SaveChangesAsync(cancellationToken);

                //index to elasticsearch
                await _dynamicEntityService.IndexAsync(entity);

                return new CreateEntityRespone
                {
                    Success = true
                };
            }

            private static T CreateValue<T, TType>(Guid entityId, Guid typeId, Guid attributeId, object value)
                where T : DynamicValue<TType>, new()
            {
                var instance = new T
                {
                    EntityId = entityId,
                    EntityTypeId = typeId,
                    AttributeId = attributeId
                };

                if (value.TryConvertTo<TType>(out var convertedValue))
                {
                    instance.Value = convertedValue;
                    return instance;
                }
                return null;
            }
        }
    }
}
