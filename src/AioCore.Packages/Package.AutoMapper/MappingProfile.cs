using AutoMapper;
using Package.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Package.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromIMap(AssemblyHelper.ExportTypes);
            ApplyMappingsFromIMapFrom(AssemblyHelper.ExportTypes);
        }

        private void ApplyMappingsFromIMap(IEnumerable<Type> types)
        {
            var instances = types
                .Where(t => typeof(IMap).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IMap>();

            foreach (var instance in instances)
            {
                instance.Mapping(this);
            }
        }

        private void ApplyMappingsFromIMapFrom(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                var sourceType = type.GetInterface(typeof(IMapFrom<>).Name)?.GetGenericArguments()[0];
                if (sourceType == null) continue;
                CreateMap(sourceType, type);
            }
        }
    }
}