using AioCore.Shared.Common;

namespace AioCore.Application.ActionProcessors
{
    public interface IActionProcessor
    {
        ActionDefinition Action { get; }
    }
}
