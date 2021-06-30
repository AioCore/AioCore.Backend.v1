using AioCore.Shared.Common;
using System.Collections.Generic;
using System.Linq;

namespace AioCore.Application.ActionProcessors
{
    public class ActionFactory
    {
        private readonly Dictionary<StepType, IActionProcessor> _processors;

        public ActionFactory(IEnumerable<IActionProcessor> processors)
        {
            _processors = processors.ToDictionary(t => t.StepType, t => t);
        }

        public IActionProcessor GetProcessor(StepType action)
        {
            return _processors.GetValueOrDefault(action);
        }
    }
}
