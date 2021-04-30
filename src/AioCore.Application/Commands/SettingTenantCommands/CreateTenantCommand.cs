using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using MediatR;
using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SettingTenantCommands
{
    [DataContract]
    public class CreateTenantCommand : IRequest<Guid>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Guid>
    {
        private readonly ISettingTenantRepository _settingTenantRepository;

        public CreateTenantCommandHandler(ISettingTenantRepository settingTenantRepository)
        {
            _settingTenantRepository = settingTenantRepository;
        }

        public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var item = _settingTenantRepository.Add(new SettingTenant(request.Name, request.Description));
            await _settingTenantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}