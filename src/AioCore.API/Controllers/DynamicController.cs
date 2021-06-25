using AioCore.Application.DynamicCommand;
using AioCore.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    public class DynamicController : AioControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(Guid componentId, [FromBody] DynamicCommand data)
        {
            data.ComponentId = componentId;
            var respone = await Mediator.Send(data);
            if (respone?.Data is FileResponse file)
            {
                return File(file.FileData, file.ContentType, file.FileName);
            }
            return Ok(respone);
        }
    }
}
