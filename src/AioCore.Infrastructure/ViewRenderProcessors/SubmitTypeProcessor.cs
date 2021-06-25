using AioCore.Application.UnitOfWorks;
using AioCore.Domain.Models;
using Package.ViewRender;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace AioCore.Infrastructure.ViewRenderProcessors
{
    public class SubmitTypeProcessor : IViewRenderProcessor
    {
        private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;
        private string _formId;
        private string _url;

        public string Type => "submit";

        public SubmitTypeProcessor(IAioCoreUnitOfWork aioCoreUnitOfWork)
        {
            _aioCoreUnitOfWork = aioCoreUnitOfWork;
        }

        public async Task<string> BuildOpeningTag(XElement element)
        {
            var @class = element.Attribute("class")?.Value;
            string id = "", text = "", title = "", icon = "";

            if (Guid.TryParse(element.Attribute("action")?.Value, out var actionId))
            {
                var component = await _aioCoreUnitOfWork.SettingComponents.FindAsync(actionId);
                if (component is not null)
                {
                    var settings = component.GetComponentSettings<ActionSettings>();
                    id = component.Id.ToString();
                    text = settings?.Text;
                    title = settings?.Title;
                    icon = settings?.Icon;
                    _formId = component.ParentId.ToString();
                    _url = settings?.ActionUrl;
                }
            }

            var strTag = new StringBuilder($"<button type='submit'");
            if (!string.IsNullOrEmpty(id))
            {
                strTag.Append($" id='{id}'");
            }
            if (!string.IsNullOrEmpty(@class))
            {
                strTag.Append($" class='{@class}'");
            }
            if (!string.IsNullOrEmpty(title))
            {
                strTag.Append($" title='{title}'");
            }
            if (!string.IsNullOrEmpty(_formId))
            {
                strTag.Append($" onclick='_{_formId.Replace("-", "")}();'");
            }
            strTag.Append('>');
            if (!string.IsNullOrEmpty(icon))
            {
                strTag.AppendLine(icon);
            }
            if (!string.IsNullOrEmpty(text))
            {
                strTag.Append(HttpUtility.HtmlEncode(text));
            }

            return strTag.ToString();
        }

        public async Task<string> BuildClosingTag(XElement element)
        {
            var strTag = new StringBuilder($"</button>");
            if (!string.IsNullOrEmpty(_formId))
            {
                strTag.AppendLine("<script type='text/javascript'>");
                strTag.AppendLine(@$"
function _{_formId.Replace("-", "")}(){{
    var form = document.getElementById('{_formId}')
    var formData = new FormData(form);
    var formValues = {{}};
    formData.forEach((value,key) => {{
        formValues[key] = value;
    }})
    var headers = new Headers();
    headers.append('Content-Type','application/json')
    var raw = JSON.stringify(formValues);
    var requestOptions = {{
        method: 'POST',
		headers: headers,
		body: raw,
		redirect: 'follow'
	}};
    
    fetch('{_url}', requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));
}}
");
                strTag.AppendLine("</script>");
            }

            return await Task.FromResult(strTag.ToString());
        }
    }
}
