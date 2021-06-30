using System;

namespace AioCore.Application.Models
{
    public class DynamicActionResult
    {
        public Guid ComponentId { get; set; }
        public Guid ActionId { get; set; }
        public Guid ActionStepId { get; set; }
        public dynamic Data { get; set; }
    }
}