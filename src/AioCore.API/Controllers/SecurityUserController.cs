using AioCore.Application.Commands.SecurityUserCommands;
using AioCore.Application.Queries.SecurityUserQueries;
using AioCore.Shared.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("{culture}/api/v1/user")]
    public class SecurityUserController : AioController
    {
        private readonly IMediator _mediator;

        public SecurityUserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("item/{userId:guid}")]
        public async Task<ActionResult<GetUserResponse>> GetTenantAsync([FromQuery] Guid userId)
        {
            if (userId.Equals(Guid.Empty)) return BadRequest();

            try
            {
                var query = new GetUserQuery();
                query.MergeParams(userId);
                var res = await _mediator.Send(query);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("items")]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 1, string keyword = null)
        {
            try
            {
                var query = new ListUserQuery();
                query.MergeParams(pageSize, pageIndex, keyword);
                var res = await _mediator.Send(query);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(id);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteUserCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}