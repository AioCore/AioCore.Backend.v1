using System;

namespace Package.NestedSet.Models
{
    public class MoveToSiblingModel
    {
        public Guid NodeId { get; set; }

        public Guid? SiblingId { get; set; }

        public NestedSetInsertMode InsertMode { get; set; }
    }
}