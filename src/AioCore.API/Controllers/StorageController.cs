using AioCore.Application.Commands.DynamicBinaryCommands;
using AioCore.Application.Queries.DynamicBinaryQueries;
using AioCore.Shared.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("{culture:alpha}/api/v1/storage")]
    public class StorageController : AioController
    {
        private readonly IMediator _mediator;

        public StorageController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("upload")]
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
        public async Task<IActionResult> File([FromQuery] Guid id)
        {
            try
            {
                var query = new GetBinaryQuery();
                query.MergeParams(id);
                var res = await _mediator.Send(query);
                return File(res.Bytes, res.ContentType, res.SourceName);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}