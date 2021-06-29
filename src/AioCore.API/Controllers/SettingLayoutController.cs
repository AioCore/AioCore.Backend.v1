using AioCore.Application.Commands.SettingLayoutCommands;
using AioCore.Application.Queries.SettingLayoutQueries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("{culture}/settings/api/v1/layout")]
    public class SettingLayoutController : AioControllerBase
    {
        [HttpGet("item")]
        public async Task<IActionResult> ItemAsync([FromQuery] Guid layoutId)
        {
            if (layoutId.Equals(Guid.Empty)) return BadRequest();

            try
            {
                var query = new GetLayoutQuery();
                query.MergeParams(layoutId);
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
                var query = new ListLayoutQuery();
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateLayoutCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateLayoutCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}