using AioCore.Application.Commands.DynamicEntityCommand;
using AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    //[AioAuthorize]
    [Authorize]
    public class DynamicEntityController : AioControllerBase
    {
        [HttpPost("create-type")]
        public async Task<ActionResult<SettingEntityType>> CreateEntityType([FromBody] CreateEntityTypeCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("create-entity")]
        public async Task<ActionResult> CreateEntity([FromBody] CreateEntityCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("update-entity")]
        public async Task<ActionResult> UpdateEntity([FromBody] UpdateEntityCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}