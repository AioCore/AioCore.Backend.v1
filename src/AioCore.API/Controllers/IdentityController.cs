using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Package.Localization;

namespace AioCore.API.Controllers
{
    [Route("{culture}/api/v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IStringLocalizer<Localization> _localizer;

        public IdentityController(IStringLocalizer<Localization> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        [Route("key")]
        public string Key(string key)
        {
            return _localizer[key];
        }
    }
}