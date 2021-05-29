using System;

namespace Package.NestedSet.Models
{
    public class MoveToParentModel
    {
        public Guid NodeId { get; set; }

        public Guid? ParentId { get; set; }

        public NestedSetInsertMode InsertMode { get; set; }
    }
}