using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemTenantResponses;
using AioCore.Application.Services;
using AioCore.Domain.CoreEntities;
using MediatR;
using Package.AutoMapper;
using Package.DatabaseManagement;
using Package.Elasticsearch;

namespace AioCore.Application.Commands.SystemTenantCommands
{
    public class CreateTenantCommand : IRequest<CreateTenantResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? FaviconId { get; set; }
        public Guid? LogoId { get; set; }
        public DatabaseInfo Database { get; set; }
        public ElasticsearchInfo Elasticsearch { get; set; }

        internal class Handler : IRequestHandler<CreateTenantCommand, CreateTenantResponse>
        {
            private readonly ITenantService _tenantService;

            public Handler(ITenantService tenantService)
            {
                _tenantService = tenantService;
            }

            public async Task<CreateTenantResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
            {
                var tenant = await _tenantService.CreateTenantAsync(new SystemTenant
                {
                    Name = request.Name,
                    Description = request.Description,
                    FaviconId = request.FaviconId,
                    LogoId = request.LogoId,
                    DatabaseInfo = request.Database.ToString(),
                    ElasticsearchInfo = request.Elasticsearch.ToString()
                }, cancellationToken);

                return tenant.MapTo<CreateTenantResponse>();
            }
        }
    }
}