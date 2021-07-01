using AioCore.Domain.Models;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;
using Plugin.ViewRender.Abstracts;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Plugin.ViewRender.Processors
{
    public class LabelTypeProcessor : IViewRenderProcessor
    {
        private readonly IAioCoreUnitOfWork _unitOfWork;

        public string Type => "label";

        public LabelTypeProcessor(IAioCoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> BuildOpeningTag(XElement element)
        {
            var name = element.Attribute("name")?.Value;
            var @class = element.Attribute("class")?.Value;
            var innerText = "";
            var hidden = false;

            if (Guid.TryParse(element.Attribute("id")?.Value, out var componentId))
            {
                var component = await _unitOfWork.SettingComponents.FindAsync(componentId);
                if (component is not null)
                {
                    var settings = component.GetComponentSettings<LabelSettings>();
                    innerText = settings?.InnerText;
                    hidden = settings?.Hidden ?? false;
                }
            }

            var strTag = new StringBuilder($"<{name}");
            if (!string.IsNullOrEmpty(@class))
            {
                strTag.Append($" class='{@class}'");
            }
            if (hidden)
            {
                strTag.Append($" style='display:none;'");
            }
            if (!string.IsNullOrEmpty(innerText))
            {
                strTag.Append($">{HttpUtility.HtmlEncode(innerText)}");
            }
            else
            {
                strTag.Append('>');
            }
            return strTag.ToString();
        }

        public async Task<string> BuildClosingTag(XElement element)
        {
            return await Task.FromResult($"</{element.Attribute("name").Value}>");
        }
    }
}