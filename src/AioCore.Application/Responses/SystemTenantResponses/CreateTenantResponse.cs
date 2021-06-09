using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using Package.AutoMapper;
using System;

namespace AioCore.Application.Responses.SystemTenantResponses
{
    public record CreateTenantResponse : IMapFrom<SystemTenant>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid FaviconId { get; set; }

        public Guid LogoId { get; set; }

        public string Server { get; set; }

        public string User { get; set; }

        public string Database { get; set; }

        public string Password { get; set; }

        public string Schema { get; set; }

        public string DatabaseType { get; set; }
    }
}