using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AioCore.API.Controllers
{
    [Route("api/v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;

        public IdentityController(IStringLocalizer localizer)
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