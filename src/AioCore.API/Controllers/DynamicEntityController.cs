using AioCore.Application.Commands.DynamicEntityCommand;
using AioCore.Application.Queries.DynamicEntityQueries;
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
        public async Task<ActionResult<CreateEntityRespone>> CreateEntity([FromBody] CreateEntityCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("update-entity")]
        public async Task<ActionResult<UpdateEntityRespone>> UpdateEntity([FromBody] UpdateEntityCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("delete-entity")]
        public async Task<ActionResult<DeleteEntityRespone>> DeleteEntity(DeleteEntityCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("entity")]
        public async Task<ActionResult> GetEntity(GetEntityQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}