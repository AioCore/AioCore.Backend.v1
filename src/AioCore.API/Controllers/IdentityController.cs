using AioCore.Application.Commands.IdentityCommands;
using AioCore.Application.UnitOfWorks;
using AioCore.Infrastructure;
using AioCore.Infrastructure.Authorize;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("{culture}/api/v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;

        public IdentityController(IMediator mediator, IServiceProvider serviceProvider)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(SignInCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Route("sign-up")]
        [AioAuthorize]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            var context = _serviceProvider.GetRequiredService<IAioDynamicUnitOfWork>();
            return Ok(context.DynamicDateValues);
            return Ok(await _mediator.Send(command));
        }
    }
}