using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace Package.Localization
{
    public static class LocalizationRegistration
    {
        public static IServiceCollection AddAioLocalization(this IServiceCollection services)
        {
            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new("en-US"),
                        new("vi-VN")
                    };

                    options.DefaultRequestCulture = new RequestCulture("vi-VN", "vi-VN");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new IRequestCultureProvider[]
                    {
                        new RouteDataRequestCultureProvider
                        {
                            IndexOfCulture = 1
                        }
                    };
                });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });

            return services;
        }

        public static IApplicationBuilder UseAioLocalization(this IApplicationBuilder app)
        {
            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            if (localizeOptions != null) app.UseRequestLocalization(localizeOptions.Value);

            return app;
        }
    }
}