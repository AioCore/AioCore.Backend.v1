using Microsoft.Extensions.DependencyInjection;

namespace Package.Redis
{
    public static class DependencyExtensions
    {
        public static void AddCacheManager(this IServiceCollection services)
        {
            services.AddSingleton<ICacheManager, RedisCacheManager>();
        }
    }
}
