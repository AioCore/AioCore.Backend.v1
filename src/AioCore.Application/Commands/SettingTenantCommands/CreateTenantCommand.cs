using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using MediatR;
using Package.Elasticsearch;
using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SettingTenantCommands
{
    [DataContract]
    public class CreateTenantCommand : IRequest<SettingTenant>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        internal class Handler : IRequestHandler<CreateTenantCommand, SettingTenant>
        {
            private readonly ISettingTenantRepository _settingTenantRepository;
            private readonly IElasticsearchService _elasticsearchService;

            public Handler(
                ISettingTenantRepository settingTenantRepository,
                IElasticsearchService elasticsearchService)
            {
                _settingTenantRepository = settingTenantRepository ?? throw new ArgumentNullException(nameof(settingTenantRepository));
                _elasticsearchService = elasticsearchService ?? throw new ArgumentNullException(nameof(elasticsearchService));
            }

            public async Task<SettingTenant> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
            {
                var item = _settingTenantRepository.Add(new SettingTenant(request.Name, request.Description));
                await _settingTenantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item;
            }
        }
    }
}