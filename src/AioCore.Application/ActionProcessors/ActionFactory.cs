using AioCore.Shared.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AioCore.Application.ActionProcessors
{
    public class ActionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<ActionDefinition, Type> _serviceTypes;

        public ActionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _serviceTypes = _serviceProvider
                .GetRequiredService<IEnumerable<IActionProcessor>>()
                .ToDictionary(t => t.Action, t => t.GetType());
        }

        public IActionProcessor GetProcessor(ActionDefinition action)
        {
            var serviceType = _serviceTypes.GetValueOrDefault(action);
            if (serviceType == null) return null;
            return _serviceProvider.GetRequiredService(serviceType) as IActionProcessor;
        }
    }
}
