using AioCore.Shared.Mvc;
using MediatR;
using System;

namespace AioCore.API.Controllers
{
    public class SettingLayoutController : AioController
    {
        private readonly IMediator _mediator;

        public SettingLayoutController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}