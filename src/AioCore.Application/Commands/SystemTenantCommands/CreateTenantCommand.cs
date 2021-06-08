using System;
using System.Threading;
using System.Threading.Tasks;
using AioCore.Application.Responses.SystemTenantResponses;
using AioCore.Application.Services;
using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using MediatR;
using Package.AutoMapper;
using Package.DatabaseManagement;

namespace AioCore.Application.Commands.SystemTenantCommands
{
    public class CreateTenantCommand : IRequest<CreateTenantResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid FaviconId { get; set; }
        public Guid LogoId { get; set; }
        public string Database { get; set; }
        public string Password { get; set; }
        public string Schema { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public DatabaseType DatabaseType { get; set; }

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
                    Database = request.Database,
                    Password = request.Password,
                    Schema = request.Schema,
                    Server = request.Server,
                    User = request.User,
                    DatabaseType = request.DatabaseType.ToString()
                }, cancellationToken);

                return tenant.MapTo<CreateTenantResponse>();
            }
        }
    }
}