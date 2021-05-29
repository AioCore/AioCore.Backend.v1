using System;

namespace Package.NestedSet.Models
{
    public class InsertNextTo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? SiblingId { get; set; }

        public NestedSetInsertMode InsertMode { get; set; }
    }
}