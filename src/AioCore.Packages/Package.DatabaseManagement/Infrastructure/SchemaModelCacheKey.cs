using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Package.DatabaseManagement.Infrastructure
{
    internal class SchemaModelCacheKey : ModelCacheKey
    {
        private readonly string _schema;

        public SchemaModelCacheKey(DbContext context, string schema) : base(context)
        {
            _schema = schema;
        }

        public override int GetHashCode() => string.IsNullOrEmpty(_schema) ? base.GetHashCode() : _schema.GetHashCode();
    }
}
