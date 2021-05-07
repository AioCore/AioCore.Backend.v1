using AioCore.Application.Commands.DynamicBinaryCommands;
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
    }
}