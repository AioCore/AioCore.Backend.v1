using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SettingTenantCommands
{
    public class CreateTenantCommand : IRequest<bool>
    {
        public string Name { get; set; }

        internal class Handler : IRequestHandler<CreateTenantCommand, bool>
        {
            private readonly ISettingTenantRepository _settingTenantRepository;

            public Handler(ISettingTenantRepository settingTenantRepository)
            {
                _settingTenantRepository = settingTenantRepository;
            }

            public Task<bool> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}