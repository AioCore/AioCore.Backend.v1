using AioCore.Application.Commands.IdentityCommands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    public class IdentityController : AioControllerBase
    {
        [HttpPost("presign-in")]
        public async Task<IActionResult> PreSignIn(PreSiginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}