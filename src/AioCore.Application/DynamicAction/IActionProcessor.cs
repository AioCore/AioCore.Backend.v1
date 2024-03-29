﻿using AioCore.Application.Models;
using AioCore.Application.Plugin;
using AioCore.Shared.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.DynamicAction
{
    public interface IActionProcessor : IPlugin
    {
        StepType StepType { get; }

        Task<Dictionary<string, object>> ExecuteAsync(DynamicActionModel actionModel, CancellationToken cancellationToken);
    }
}
