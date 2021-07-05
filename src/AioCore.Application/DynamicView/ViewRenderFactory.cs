using System.Collections.Generic;
using System.Linq;

namespace AioCore.Application.DynamicView
{
    public class ViewRenderFactory
    {
        private readonly Dictionary<string, IViewRenderProcessor> _processors;

        public ViewRenderFactory(IEnumerable<IViewRenderProcessor> processors)
        {
            _processors = processors.ToDictionary(t => t.Type, t => t);
        }

        public IViewRenderProcessor GetProcessor(string type)
        {
            return _processors.GetValueOrDefault(type);
        }
    }
}