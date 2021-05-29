using System;

namespace Package.NestedSet.Models
{
    public class DeleteNodeModel
    {
        public Guid NodeId { get; set; }

        public bool Soft { get; set; }
    }
}