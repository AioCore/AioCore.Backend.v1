using System;

namespace AioCore.Application.DynamicCommand
{
    public class DynamicActionResponse
    {
        public Guid ComponentId { get; set; }
        public Guid ActionId { get; set; }
        public Guid ActionStepId { get; set; }
        public dynamic Data { get; set; }
    }
}
