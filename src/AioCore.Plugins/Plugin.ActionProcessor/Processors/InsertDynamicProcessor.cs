﻿using AioCore.Application.DynamicAction;
using AioCore.Application.Models;
using AioCore.Shared.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.ActionProcessor.Processors
{
    public class InsertDynamicProcessor : IActionProcessor
    {
        public StepType StepType => StepType.Create;

        public Task<Dictionary<string, object>> ExecuteAsync(DynamicActionModel actionModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}