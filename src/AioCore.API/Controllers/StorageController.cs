using AioCore.Application.Commands.DynamicBinaryCommands;
using AioCore.Application.Queries.DynamicBinaryQueries;
using AioCore.Application.Queries.SettingTenantQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("api/storage/v1/binary")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("upload")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Upload([FromForm] CreateBinaryCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("file")]
        [ProducesResponseType(typeof(GetTenantResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> File([FromQuery] Guid id)
        {
            try
            {
                var query = new GetBinaryQuery();
                query.MergeParams(id);
                var res = await _mediator.Send(query);
                return File(res.Bytes, res.ContentType, res.FileName);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}