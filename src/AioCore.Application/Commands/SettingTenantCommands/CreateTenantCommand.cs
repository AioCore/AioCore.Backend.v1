using AioCore.Application.Responses.SettingTenantResponses;
using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.Commands.SettingTenantCommands
{
    public class CreateTenantCommand : IRequest<CreateTenantResponse>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        internal class Handler : IRequestHandler<CreateTenantCommand, CreateTenantResponse>
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

            public async Task<CreateTenantResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
            {
                var item = _settingTenantRepository.Add(new SettingTenant(request.Name, request.Description));
                await _settingTenantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item.To<CreateTenantResponse>();
            }
        }
    }
}