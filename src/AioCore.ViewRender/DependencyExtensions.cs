using AioCore.Application.ViewRender;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AioCore.ViewRender
{
    public static class DependencyExtensions
    {
        public static void AddViewRender(this IServiceCollection services)
        {
            services.AddScoped<HtmlBuilder>();
            services.AddSingleton<ViewRenderFactory>();

            var types = typeof(DependencyExtensions).Assembly.GetTypes()
                .Where(t => typeof(IViewRenderProcessor).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
            foreach (var type in types)
            {
                services.AddScoped(typeof(IViewRenderProcessor), type);
                services.AddScoped(type);
            }
        }
    }
}
