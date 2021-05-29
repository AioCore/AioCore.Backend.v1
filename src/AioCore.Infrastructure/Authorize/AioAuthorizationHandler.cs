using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AioCore.Infrastructure.Authorize
{
    public class AioAuthorizationHandler : AuthorizationHandler<AioPolicyRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AioPolicyRequirement requirement)
        {
            if (context.User.Identity is { IsAuthenticated: false })
            {
                context.Fail();
                return;
            }

            var policyClaims = context.User.FindFirst(t => t.Type == "policies")?.Value;

            if (string.IsNullOrEmpty(policyClaims))
            {
                context.Fail();
                return;
            }

            var httpContext = (HttpContext)context.Resource;
            if (httpContext != null)
            {
                var routeValues = httpContext.Request.RouteValues;
                var controller = routeValues["controller"]?.ToString();
                var action = routeValues["action"]?.ToString();

                var policies = policyClaims.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(t =>
                {
                    var arr = t.Split("|", StringSplitOptions.RemoveEmptyEntries);
                    return new
                    {
                        Controller = arr[0],
                        Action = arr[1]
                    };
                });

                if (policies.Any(t => t.Controller == controller && t.Action == action))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }

            await Task.CompletedTask;
        }
    }
}