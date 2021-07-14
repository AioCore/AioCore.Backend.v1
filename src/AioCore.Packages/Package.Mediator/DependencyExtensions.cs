using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Package.Mediator
{
    public static class DependencyExtensions
    {
        public static void AddMediator(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(options => options.AsScoped(), assemblies);
            services.AddScoped<Publisher>();

            var types = assemblies.SelectMany(asm => asm.GetExportedTypes().Where(t =>
                t.IsAssignableToGenericType(typeof(IPipelineBehavior<,>)) &&
                !t.IsAbstract &&
                !t.IsInterface)
            );
            foreach (var type in types)
            {
                services.AddScoped(typeof(IPipelineBehavior<,>), type);
            }
        }

        private static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType == null || genericType == null)
            {
                return false;
            }

            return givenType == genericType
              || givenType.MapsToGenericTypeDefinition(genericType)
              || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
              || givenType.BaseType.IsAssignableToGenericType(genericType);
        }

        private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return givenType
              .GetInterfaces()
              .Where(it => it.IsGenericType)
              .Any(it => it.GetGenericTypeDefinition() == genericType);
        }

        private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return genericType.IsGenericTypeDefinition
              && givenType.IsGenericType
              && givenType.GetGenericTypeDefinition() == genericType;
        }
    }
}
