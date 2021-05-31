using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Package.DatabaseManagement.Infrastructure
{
    internal class DbSchemaAwareModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
        {
            return new SchemaModelCacheKey(context, context is ISchemaDbContext schema ? schema.Schema : null);
        }
    }
}
