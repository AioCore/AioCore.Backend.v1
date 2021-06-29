using AioCore.Shared.Common;
using System;

namespace AioCore.Application.Models
{
    public class AttributeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DataType DataType { get; set; }
    }
}
