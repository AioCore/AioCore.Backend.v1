using System.Collections.Generic;
using System.Linq;

namespace Package.NestedSet
{
    public interface INestedSetManager<T, in TKey, in TNullableKey>
    {
        IEnumerable<T> Delete(TKey nodeId, bool soft = false);

        void MoveToParent(TKey nodeId, TNullableKey parentId,
            NestedSetInsertMode insertMode);

        void MoveToSibling(TKey nodeId, TNullableKey siblingId,
            NestedSetInsertMode insertMode);

        T InsertRoot(T node,
            NestedSetInsertMode insertMode);

        List<T> InsertRoot(IEnumerable<T> nodeTree,
            NestedSetInsertMode insertMode);

        T InsertBelow(TNullableKey parentId, T node,
            NestedSetInsertMode insertMode);

        List<T> InsertBelow(TNullableKey parentId, IEnumerable<T> nodeTree,
            NestedSetInsertMode insertMode);

        T InsertNextTo(TNullableKey siblingId, T node,
            NestedSetInsertMode insertMode);

        T InsertNextTo(TNullableKey siblingId, IEnumerable<T> nodeTree,
            NestedSetInsertMode insertMode);

        IQueryable<T> GetDescendants(TKey nodeId, int? depth = null);

        IQueryable<T> GetImmediateChildren(TKey nodeId);

        IOrderedEnumerable<T> GetPathToNode(TKey nodeId);

        IOrderedEnumerable<T> GetPathToNode(T node);
    }
}