using AioCore.Application.ActionProcessors;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AioCore.ActionProcessors
{
    public static class DependencyExtensions
    {
        public static void AddActionProcessors(this IServiceCollection services)
        {
            services.AddSingleton<ActionFactory>();
            var types = typeof(DependencyExtensions).Assembly.GetTypes()
                .Where(t => typeof(IActionProcessor).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
            foreach (var type in types)
            {
                services.AddScoped(typeof(IActionProcessor), type);
                services.AddScoped(type);
            }
        }
    }
}
