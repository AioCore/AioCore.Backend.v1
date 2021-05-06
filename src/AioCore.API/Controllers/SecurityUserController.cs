using AioCore.Application.Commands.SecurityUserCommands;
using AioCore.Application.Queries.SecurityUserQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("api/security/v1/user")]
    [ApiController]
    public class SecurityUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SecurityUserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("items/{userId:guid}")]
        [ProducesResponseType(typeof(GetUserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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

        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(ListUserQuery), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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

        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}