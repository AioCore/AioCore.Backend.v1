using AioCore.Application.Responses.SystemTenantResponses;
using AioCore.Domain.CoreEntities;
using MediatR;
using Package.AutoMapper;
using Package.DatabaseManagement;
using Package.Elasticsearch;
using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Services;

namespace AioCore.Application.Commands.SystemTenantCommands
{
    public class UpdateTenantCommand : IRequest<CreateTenantResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? FaviconId { get; set; }
        public Guid? LogoId { get; set; }
        public DatabaseSettings Database { get; set; }
        public ElasticsearchInfo Elasticsearch { get; set; }

        internal class Handler : IRequestHandler<UpdateTenantCommand, CreateTenantResponse>
        {
            private readonly ITenantService _tenantService;

            public Handler(ITenantService tenantService)
            {
                _tenantService = tenantService;
            }

            public async Task<CreateTenantResponse> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
            {
                var tenant = await _tenantService.UpdateTenantAsync(new SystemTenant
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    FaviconId = request.FaviconId,
                    LogoId = request.LogoId,
                    DatabaseSettingsJson = request.Database.ToString(),
                    ElasticsearchSettingsJson = request.Elasticsearch.ToString()
                }, cancellationToken);

                return tenant.MapTo<CreateTenantResponse>();
            }
        }
    }
}