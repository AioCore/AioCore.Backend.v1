using AioCore.Application.Models;
using AioCore.Shared.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.Application.ActionProcessors
{
    public interface IActionProcessor
    {
        StepType StepType { get; }

        Task<Dictionary<string, object>> ExecuteAsync(DynamicActionModel actionModel, CancellationToken cancellationToken);
    }
}
