using System;
using System.Collections.Generic;
using System.Linq;
using AioCore.Shared.Common;
using Microsoft.Extensions.DependencyInjection;
using Plugin.ActionProcessor.Abstracts;

namespace Plugin.ActionProcessor
{
    public class ActionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<StepType, Type> _serviceTypes;

        public ActionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            _serviceTypes = _serviceProvider
                .GetRequiredService<IEnumerable<IActionProcessor>>()
                .ToDictionary(t => t.StepType, t => t.GetType());
        }

        public IActionProcessor GetProcessor(StepType action)
        {
            var serviceType = _serviceTypes.GetValueOrDefault(action);
            if (serviceType == null) return null;
            return _serviceProvider.GetRequiredService(serviceType) as IActionProcessor;
        }
    }
}