using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemTenantResponses;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using MediatR;
using Package.AutoMapper;
using Package.Elasticsearch;

namespace AioCore.Application.Commands.SystemTenantCommands
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
                var item = _settingTenantRepository.Add(new SystemTenant(request.Name, request.Description));
                await _settingTenantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _elasticsearchService.IndexAsync(item);
                return item.To<CreateTenantResponse>();
            }
        }
    }
}