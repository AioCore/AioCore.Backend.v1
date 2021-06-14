using AioCore.Application.Repositories;
using AioCore.Domain.SettingAggregatesModel.SettingFeatureAggregate;
using AioCore.Infrastructure.DbContexts;

namespace AioCore.Infrastructure.Repositories
{
    public class SettingFeatureRepository : Repository<SettingFeature>, ISettingFeatureRepository
    {
        public SettingFeatureRepository(AioCoreContext context) : base(context)
        {
        }
    }
}