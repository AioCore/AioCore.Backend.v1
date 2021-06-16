using AioCore.Application.UnitOfWorks;
using AioCore.Domain.DynamicAggregatesModel;
using AioCore.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.DynamicEntityCommand
{
    public class UpdateEntityTypeCommand : IRequest<UpdateEntityTypeRespone>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AttributeModel> Atributes { get; set; } = new List<AttributeModel>();

        internal class Handler : IRequestHandler<UpdateEntityTypeCommand, UpdateEntityTypeRespone>
        {
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;
            private readonly IAioDynamicUnitOfWorkFactory _dynamicUnitOfWorkFactory;

            public Handler(
                  IAioCoreUnitOfWork coreUnitOfWork
                , IAioDynamicUnitOfWorkFactory dynamicUnitOfWorkFactory)
            {
                _coreUnitOfWork = coreUnitOfWork;
                _dynamicUnitOfWorkFactory = dynamicUnitOfWorkFactory;
            }

            public async Task<UpdateEntityTypeRespone> Handle(UpdateEntityTypeCommand request, CancellationToken cancellationToken)
            {
                var entityType = await _coreUnitOfWork.SettingEntityTypes.FindAsync(new object[] { request.Id }, cancellationToken);
                if (entityType is null)
                {
                    throw new AioCoreException("Current entity type does not exists");
                }

                var dynamicUnitOfWork = await _dynamicUnitOfWorkFactory.CreateUnitOfWorkAsync(entityType.TenantId, cancellationToken);

                entityType.Name = request.Name;
                entityType.Description = request.Description;
                await _coreUnitOfWork.SaveChangesAsync(cancellationToken);

                var attributes = await dynamicUnitOfWork.DynamicAttributes
                    .Where(t => t.EntityTypeId == entityType.Id)
                    .ToListAsync(cancellationToken);

                foreach (var attr in attributes)
                {
                    var updateAttr = request.Atributes.FirstOrDefault(t => t.Id == attr.Id);
                    if (updateAttr is null) continue;
                    attr.Name = updateAttr.Name;
                }

                var addAttrs = request.Atributes.Where(t => !attributes.Any(x => x.Name == t.Name) && t.Id == Guid.Empty);
                dynamicUnitOfWork.DynamicAttributes.AddRange(addAttrs.Select(t => new DynamicAttribute
                {
                    Name = t.Name,
                    EntityTypeId = entityType.Id,
                    DataType = t.DataType
                }));

                await dynamicUnitOfWork.SaveChangesAsync(cancellationToken);

                return new UpdateEntityTypeRespone { Success = true };
            }
        }
    }
}
