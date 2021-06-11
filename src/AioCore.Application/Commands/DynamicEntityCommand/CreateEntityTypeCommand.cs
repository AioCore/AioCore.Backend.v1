using AioCore.Application.UnitOfWorks;
using AioCore.Domain.DynamicAggregatesModel;
using AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.DynamicEntityCommand
{
    public class CreateEntityTypeCommand : IRequest<SettingEntityType>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TenantId { get; set; }
        public List<AttributeModel> Atributes { get; set; }


        internal class Handler : IRequestHandler<CreateEntityTypeCommand, SettingEntityType>
        {
            private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;
            private readonly IAioDynamicUnitOfWorkFactory _aioDynamicUnitOfWorkFactory;

            public Handler(
                  IAioCoreUnitOfWork aioCoreUnitOfWork
                , IAioDynamicUnitOfWorkFactory aioDynamicUnitOfWorkFactory)
            {
                _aioCoreUnitOfWork = aioCoreUnitOfWork;
                _aioDynamicUnitOfWorkFactory = aioDynamicUnitOfWorkFactory;
            }

            public async Task<SettingEntityType> Handle(CreateEntityTypeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _aioCoreUnitOfWork.SettingEntityTypes.AddAsync(new SettingEntityType
                {
                    Name = request.Name,
                    Description = request.Description,
                    TenantId = request.TenantId
                }, cancellationToken);
                await _aioCoreUnitOfWork.SaveChangesAsync(cancellationToken);

                var dynamicAttrs = request.Atributes.Select(t => new DynamicAttribute
                {
                    Name = t.Name,
                    EntityTypeId = entity.Id,
                    DataType = t.DataType.ToString()
                });

                var aioDynamicUnitOfWork = await _aioDynamicUnitOfWorkFactory.CreateUnitOfWorkAsync(request.TenantId);

                await aioDynamicUnitOfWork.DynamicAttributes.AddRangeAsync(dynamicAttrs, cancellationToken);
                await aioDynamicUnitOfWork.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
