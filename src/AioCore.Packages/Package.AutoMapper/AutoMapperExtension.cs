using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Package.AutoMapper
{
    public static class AutoMapperExtension
    {
        private static IMapper _mapper;

        public static void AddAutoMapper(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            static void IgnoreUnmapped(IProfileExpression cfg)
            {
                cfg.ForAllMaps((map, expr) =>
                {
                    if (map.Profile.Name != typeof(MapperConfigurationExpression).FullName) return;
                    foreach (var prop in map.GetUnmappedPropertyNames())
                    {
                        if (map.SourceType.GetProperty(prop) != null)
                        {
                            expr.ForSourceMember(prop, opt => opt.DoNotValidate());
                        }
                        if (map.DestinationType.GetProperty(prop) != null)
                        {
                            expr.ForMember(prop, opt => opt.Ignore());
                        }
                    }
                });
            }

            _mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(assemblies);
                    IgnoreUnmapped(cfg);
                })
                .CreateMapper();

            services.AddSingleton(_mapper);
        }

        public static T To<T>(this object source)
        {
            return _mapper.Map<T>(source);
        }
    }
}