using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AioCore.Infrastructure.Authorize
{
    public static class DependencyInjectionExtension
    {
        public static void AddAioAuthorize(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, AioPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, AioAuthorizationHandler>();

            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Tokens:Issuer"],
                        ValidAudience = configuration["Tokens:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]))
                    };
                });
        }
    }
}