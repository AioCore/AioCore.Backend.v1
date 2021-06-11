using AioCore.Application.UnitOfWorks;
using AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.DynamicEntityCommand
{
    public class CreateEntityTypeCommand : IRequest<SettingEntityType>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TenantId { get; set; }

        internal class Handler : IRequestHandler<CreateEntityTypeCommand, SettingEntityType>
        {
            private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;

            public Handler(IAioCoreUnitOfWork aioCoreUnitOfWork)
            {
                _aioCoreUnitOfWork = aioCoreUnitOfWork;
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

                return entity;
            }
        }
    }
}
