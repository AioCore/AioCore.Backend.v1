using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using AioCore.Domain.SettingAggregatesModel.SettingComponentAggregate;
using Microsoft.AspNetCore.Http;
using Package.ViewRender;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace AioCore.Infrastructure.ViewRenderProcessors
{
    public class FieldTypeProcessor : IViewRenderProcessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDynamicEntityService _dynamicEntityService;
        private readonly IAioCoreUnitOfWork _aioCoreUnitOfWork;

        public string Type => "field";

        public FieldTypeProcessor(
              IHttpContextAccessor httpContextAccessor
            , IDynamicEntityService dynamicEntityService
            , IAioCoreUnitOfWork aioCoreUnitOfWork
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _dynamicEntityService = dynamicEntityService;
            _aioCoreUnitOfWork = aioCoreUnitOfWork;
        }

        public async Task<string> BuildOpeningTag(XElement element)
        {
            var name = element.Attribute("name")?.Value;
            var id = "";
            var @class = element.Attribute("class")?.Value;
            object value = null;
            var placeHolder = "";
            var hidden = false;
            var title = "";
            DataType? type = null;

            Dictionary<string, object> entity = null;
            if (_httpContextAccessor.HttpContext != null && Guid.TryParse(_httpContextAccessor.HttpContext.Request.Query["id"], out var entityId))
            {
                entity = await _dynamicEntityService.GetDynamicEntityAsync(entityId);
            }

            if (Guid.TryParse(element.Attribute("id")?.Value, out var componentId))
            {
                var component = await _aioCoreUnitOfWork.SettingComponents.FindAsync(componentId);
                if (component is not null)
                {
                    var settings = component.GetComponentSettings<FieldSettings>();
                    id = component.Id.ToString();
                    title = settings.Caption;
                    placeHolder = settings.PlaceHolder;
                    hidden = settings?.Hidden ?? false;
                    value = entity?.GetValueOrDefault(component.Name);
                    type = settings.DataType;
                }
            }

            var strTag = new StringBuilder($"<{name}");
            if (!string.IsNullOrEmpty(id))
            {
                strTag.Append($" id='{id}'");
            }
            if (!string.IsNullOrEmpty(@class))
            {
                strTag.Append($" class='{@class}'");
            }
            if (hidden)
            {
                strTag.Append($" style='display:none;'");
            }
            if (!string.IsNullOrEmpty(title))
            {
                strTag.Append($" title='{title}'");
            }
            if (!string.IsNullOrEmpty(placeHolder))
            {
                strTag.Append($" placeholder='{placeHolder}'");
            }
            if (type == DataType.MultilineText)
            {
                strTag.Append($">{HttpUtility.HtmlEncode(value?.ToString() ?? "")}");
            }
            else if (type is not null)
            {
                switch (type)
                {
                    case DataType.Date:
                    case DataType.DateTime:
                        strTag.Append($" type='date'");
                        break;

                    case DataType.Text:
                        strTag.Append($" type='text'");
                        break;

                    case DataType.Password:
                        strTag.Append($" type='password'");
                        break;

                    case DataType.EmailAddress:
                        strTag.Append($" type='email'");
                        break;

                    case DataType.Upload:
                        strTag.Append($" type='file'");
                        break;
                }
                if (value is not null)
                {
                    strTag.Append($" value='{value}'");
                }
                strTag.Append('>');
            }
            else
            {
                strTag.Append('>');
            }
            return strTag.ToString();
        }

        public async Task<string> BuildClosingTag(XElement element)
        {
            return await Task.FromResult($"</{element.Attribute("name")?.Value}>");
        }
    }
}