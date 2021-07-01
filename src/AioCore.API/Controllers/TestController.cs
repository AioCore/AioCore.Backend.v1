using AioCore.Domain.Models;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;
using AioCore.Mediator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plugin.ViewRender;
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
        public async Task<IActionResult> TestNotify([FromQuery] A noti, CancellationToken cancellationToken)
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
            private readonly IAioCoreUnitOfWork _coreUnitOfWork;

            public Handler(IAioCoreUnitOfWork coreUnitOfWork)
            {
                _coreUnitOfWork = coreUnitOfWork;
            }

            public async Task Handle(A notification, CancellationToken cancellationToken)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("=========================================================================================================================Start" + notification.Text);
                        var query = from t1 in _coreUnitOfWork.SettingComponents.Where(t => t.ComponentType == ComponentType.Action)
                                    join t2 in _coreUnitOfWork.SettingActions on t1.ParentId equals t2.Id
                                    select t2;
                        var a = await query.ToListAsync(cancellationToken);
                        Console.WriteLine("=========================================================================================================================End" + notification.Text);
                        await Task.Delay(3000, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }
    }
}