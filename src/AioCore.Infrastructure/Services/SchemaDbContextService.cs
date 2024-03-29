﻿using Microsoft.AspNetCore.Http;
using Package.DatabaseManagement;

namespace AioCore.Infrastructure.Services
{
    public class SchemaDbContextService : ISchemaDbContext
    {
        public string Schema { get; }

        public SchemaDbContextService(IHttpContextAccessor httpContextAccessor)
        {
            Schema = httpContextAccessor.HttpContext?.User?.FindFirst("schema_creating")?.Value
                  ?? httpContextAccessor.HttpContext?.User?.FindFirst("schema")?.Value;
        }
    }
}
