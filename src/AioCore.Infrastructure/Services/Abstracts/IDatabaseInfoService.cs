using Package.DatabaseManagement;

namespace AioCore.Infrastructure.Services.Abstracts
{
    public interface IDatabaseInfoService
    {
        DatabaseInfo GetDatabaseInfo();
    }
}