using Microsoft.AspNetCore.Mvc;
using Package.ViewRender;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly HtmlBuilder _htmlBuilder;

        public TestController(HtmlBuilder htmlBuilder)
        {
            _htmlBuilder = htmlBuilder;
        }

        [HttpPost("testViewEngine")]
        public async Task<IActionResult> TestViewEngine()
        {
            using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
            return Ok(await _htmlBuilder.Build(await reader.ReadToEndAsync()));
        }
    }
}