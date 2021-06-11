using AioCore.Application.Commands.DynamicEntityCommand;
using AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    public class DynamicEntityController : AioControllerBase
    {
        [HttpPost("create-type")]
        public async Task<ActionResult<SettingEntityType>> CreateEntityType(CreateEntityTypeCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


    }
}