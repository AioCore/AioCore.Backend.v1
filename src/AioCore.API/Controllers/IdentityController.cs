using AioCore.Application.Commands.IdentityCommands;
using AioCore.Application.UnitOfWorks;
using AioCore.Infrastructure;
using AioCore.Infrastructure.Authorize;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("{culture}/api/v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(SignInCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}