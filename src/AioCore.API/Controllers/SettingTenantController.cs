using AioCore.Application.Queries.SettingTenantQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class SettingTenantController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SettingTenantController> _logger;

        public SettingTenantController(IMediator mediator, ILogger<SettingTenantController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("{tenantId:guid}")]
        [HttpGet]
        [ProducesResponseType(typeof(GetTenantResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetTenantAsync(GetTenantQuery query, Guid tenantId)
        {
            try
            {
                query.Id = tenantId;
                var res = await _mediator.Send(query);
                return Ok(res);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}