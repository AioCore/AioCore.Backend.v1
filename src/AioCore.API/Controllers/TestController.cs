using AioCore.Application.UnitOfWorks;
using AioCore.Application.ViewRender;
using AioCore.Domain.Models;
using AioCore.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly HtmlBuilder _htmlBuilder;
        private readonly IAioCoreUnitOfWork _coreUnitOfWork;
        private readonly IMediator _mediator;
        private readonly Publisher _publisher;

        public TestController(HtmlBuilder htmlBuilder, IAioCoreUnitOfWork coreUnitOfWork, IMediator mediator, Publisher publisher)
        {
            _htmlBuilder = htmlBuilder;
            _coreUnitOfWork = coreUnitOfWork;
            _mediator = mediator;
            _publisher = publisher;
        }

        [HttpPost("testViewEngine")]
        public async Task<IActionResult> TestViewEngine()
        {
            using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
            return Ok(await _htmlBuilder.Build(await reader.ReadToEndAsync()));
        }

        [HttpGet("testEF")]
        public async Task<IActionResult> TestEF(CancellationToken cancellationToken)
        {
            var query = from t1 in _coreUnitOfWork.SettingComponents.Where(t => t.ComponentType == ComponentType.Action)
                        join t2 in _coreUnitOfWork.SettingActions on t1.ParentId equals t2.Id
                        select t2;
            var a = await query
                .ToListAsync(cancellationToken);

            return Ok(a);
        }

        [HttpGet("testNotification")]
        public async Task<IActionResult> TestNotify([FromQuery]A noti)
        {
            await _publisher.Publish(noti, PublishStrategy.ParallelNoWait);
            return Ok();
        }
    }

    public class A : INotification
    {
        public string Text { get; set; }

        internal class Handler : INotificationHandler<A>
        {
            public async Task Handle(A notification, CancellationToken cancellationToken)
            {
                while (true)
                {
                    Console.WriteLine($"{notification.Text}. {DateTime.Now:dd/MM/yyyy}");
                    await Task.Delay(1000, cancellationToken);
                }
            }
        }
    }
}