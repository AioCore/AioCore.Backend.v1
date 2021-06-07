using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Package.ViewRender
{
    public class HtmlBuilder
    {
        private readonly ViewRenderFactory _viewRenderFactory;

        public HtmlBuilder(ViewRenderFactory viewRenderFactory)
        {
            _viewRenderFactory = viewRenderFactory;
        }

        public async Task<string> Build(string xml)
        {
            var xElement = XElement.Parse(xml);

            return await BuildFromRootElement(xElement);
        }

        private async Task<string> BuildFromRootElement(XElement root)
        {
            var stringBuilder = new StringBuilder();
            foreach (var elem in root.Elements())
            {
                stringBuilder.Append(await BuildElement(elem));
            }
            return stringBuilder.ToString().Trim();
        }

        private async Task<string> BuildElement(XElement xElement)
        {
            var type = xElement.Attribute("type")?.Value;
            var processor = _viewRenderFactory.GetProcessor(type);
            if (processor == null)
            {
                return string.Empty;
            }
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(await processor.BuildOpeningTag(xElement));
            foreach (var elem in xElement.Elements())
            {
                stringBuilder.Append(await BuildElement(elem));
            }
            stringBuilder.AppendLine(await processor.BuildClosingTag(xElement));
            return stringBuilder.ToString();
        }
    }
}