using AioCore.Application.Commands.SettingTenantCommands;
using AioCore.Application.Queries.SettingTenantQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("api/settings/v1/tenant")]
    [ApiController]
    public class SettingTenantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingTenantController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("items/{tenantId:guid}")]
        [ProducesResponseType(typeof(GetTenantResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetTenantResponse>> GetTenantAsync([FromQuery] Guid tenantId)
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

        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(ListTenantQuery), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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

        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateTenantCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}