﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AioCore.Shared.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}