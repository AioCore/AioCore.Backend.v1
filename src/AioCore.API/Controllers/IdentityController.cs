using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AioCore.API.Controllers
{
    [Route("api/v1/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn()
        {
            return Ok();
        }

        [HttpPost]
        [Route("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            return Ok();
        }
    }
}