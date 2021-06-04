using Microsoft.AspNetCore.Http;
using Package.DatabaseManagement;

namespace AioCore.Application.Services.Implements
{
    public class SchemaDbContextService : ISchemaDbContext
    {
        public string Schema { get; }

        public SchemaDbContextService(IHttpContextAccessor httpContextAccessor)
        {
            Schema = httpContextAccessor.HttpContext?.User?.FindFirst("schema")?.Value;
        }
    }
}
