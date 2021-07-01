using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Plugin.ActionProcessor.Abstracts;

namespace Plugin.ActionProcessor
{
    public static class DependencyExtensions
    {
        public static void AddDynamicAction(this IServiceCollection services)
        {
            services.AddScoped<ActionFactory>();
            var types = typeof(DependencyExtensions).Assembly.GetTypes()
                .Where(t => typeof(IActionProcessor).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
            foreach (var type in types)
            {
                services.AddScoped(typeof(IActionProcessor), type);
            }
        }
    }
}