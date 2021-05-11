using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;

namespace AioCore.API.Controllers
{
    [Route("api/v1/configs")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly IStringLocalizer<ConfigsController> _localizer;

        public ConfigsController(IStringLocalizer<ConfigsController> localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        [HttpGet("set-culture")]
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));
            }

            return LocalRedirect(redirectUri);
        }

        [HttpGet("key")]
        public string Key(string key)
        {
            return _localizer[key];
        }
    }
}