using AioCore.Shared.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AioCore.Application.Commands.SystemTenantCommands;
using AioCore.Application.Queries.SystemTenantQueries;

namespace AioCore.API.Controllers
{
    [Route("{culture}/settings/api/v1/tenant")]
    public class SettingTenantController : AioController
    {
        private readonly IMediator _mediator;

        public SettingTenantController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("item")]
        public async Task<IActionResult> ItemAsync([FromQuery] Guid tenantId)
        {
            if (tenantId.Equals(Guid.Empty)) return BadRequest();

            try
            {
                var query = new GetTenantQuery();
                query.MergeParams(tenantId);
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
                var query = new ListTenantQuery();
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateTenantCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users([FromQuery] Guid tenantId)
        {
            try
            {
                var query = new ListTenantUsersQuery();
                query.MergeParams(tenantId);
                var users = await _mediator.Send(query);
                return Ok(users);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}