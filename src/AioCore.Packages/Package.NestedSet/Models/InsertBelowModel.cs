using System;

namespace Package.NestedSet.Models
{
    public class InsertBelowModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? ParentId { get; set; }

        public NestedSetInsertMode InsertMode { get; set; }
    }
}