using System.Collections.Generic;

namespace Package.ViewRender
{
    public class ViewRenderFactory
    {
        private readonly Dictionary<string, IViewRenderProcessor> _processors;

        public ViewRenderFactory(IEnumerable<IViewRenderProcessor> processors)
        {
            if (processors == null) return;
            _processors = new Dictionary<string, IViewRenderProcessor>();
            foreach (var processor in processors)
            {
                if (_processors.ContainsKey(processor.Type)) continue;
                _processors.Add(processor.Type, processor);
            }
        }

        public IViewRenderProcessor GetProcessor(string type)
        {
            return _processors.GetValueOrDefault(type);
        }
    }
}
