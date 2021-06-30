using AioCore.Domain.CoreEntities;
using System;

namespace AioCore.Application.Models
{
    public class DynamicActionResult
    {
        public Guid ContainerId { get; set; }
        public Guid ActionId { get; set; }
        public Guid ActionStepId { get; set; }
        public dynamic Data { get; set; }
    }
}
