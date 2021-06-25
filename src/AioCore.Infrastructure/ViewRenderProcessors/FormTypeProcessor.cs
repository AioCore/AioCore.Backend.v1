using AioCore.Application.UnitOfWorks;
using AioCore.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Package.ViewRender;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AioCore.Infrastructure.ViewRenderProcessors
{
    public class FormTypeProcessor : IViewRenderProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;

        public string Type => "form";

        public FormTypeProcessor(
            IServiceProvider serviceProvider
            , IAioCoreUnitOfWork aioCoreUnitOfWork
        )
        {
            _serviceProvider = serviceProvider;
            _aioCoreUnitOfWork = aioCoreUnitOfWork;
        }

        public async Task<string> BuildOpeningTag(XElement element)
        {
            string id = "", method = "", enctype = "", innerText = "";
            var @class = element.Attribute("class")?.Value;

            if (Guid.TryParse(element.Attribute("id")?.Value, out var componentId))
            {
                var component = await _aioCoreUnitOfWork.SettingComponents.FindAsync(componentId);
                if (component is not null)
                {
                    var settings = component.GetComponentSettings<FormSettings>();
                    id = component?.Id.ToString();
                    method = settings?.Method ?? "post";
                    enctype = settings?.Enctype ?? "";
                    innerText = settings?.InnerText ?? "";
                }
            }

            var strTag = new StringBuilder($"<form");
            if (!string.IsNullOrEmpty(id))
            {
                strTag.Append($" id='{id}'");
            }
            if (!string.IsNullOrEmpty(method))
            {
                strTag.Append($" method='{method}'");
            }
            if (!string.IsNullOrEmpty(@class))
            {
                strTag.Append($" class='{@class}'");
            }
            if (!string.IsNullOrEmpty(enctype))
            {
                strTag.Append($" enctype='{enctype}'");
            }
            strTag.Append('>');
            if (!string.IsNullOrEmpty(innerText))
            {
                var htmlBuilder = _serviceProvider.GetRequiredService<HtmlBuilder>();
                strTag.AppendLine(await htmlBuilder.Build(innerText));
            }
            return strTag.ToString();
        }

        public async Task<string> BuildClosingTag(XElement element)
        {
            return await Task.FromResult($"</{element.Attribute("name").Value}>");
        }
    }
}
