using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AioCore.Application.ViewRender
{
    public class ViewRenderFactory
    {
        private readonly Dictionary<string, Type> _serviceTypes;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _serviceTypes = _serviceProvider
                .GetRequiredService<IEnumerable<IViewRenderProcessor>>()
                .ToDictionary(t => t.Type, t => t.GetType());
        }

        public IViewRenderProcessor GetProcessor(string type)
        {
            var serviceType = _serviceTypes.GetValueOrDefault(type);
            if (serviceType == null) return null;
            return _serviceProvider.GetRequiredService(serviceType) as IViewRenderProcessor;
        }
    }
}
