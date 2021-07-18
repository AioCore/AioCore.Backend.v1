using Package.DatabaseManagement;

namespace AioCore.Application.Services
{
    public interface IDatabaseInfoService
    {
        DatabaseSettings GetDatabaseInfo();
    }
}