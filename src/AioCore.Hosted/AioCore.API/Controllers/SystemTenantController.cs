using AioCore.Application.Commands.SystemTenantCommands;
using AioCore.Application.Queries.SystemTenantQueries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("{culture}/settings/api/v1/tenant")]
    public class SystemTenantController : AioControllerBase
    {
        [HttpGet("item")]
        public async Task<IActionResult> ItemAsync([FromQuery] Guid tenantId)
        {
            if (tenantId.Equals(Guid.Empty)) return BadRequest();

            try
            {
                var query = new GetTenantQuery();
                query.MergeParams(tenantId);
                var res = await Mediator.Send(query);
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
                var res = await Mediator.Send(query);
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
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTenantCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users([FromQuery] Guid tenantId)
        {
            try
            {
                var query = new ListTenantUsersQuery();
                query.MergeParams(tenantId);
                var users = await Mediator.Send(query);
                return Ok(users);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            return Ok();
        }
    }
}