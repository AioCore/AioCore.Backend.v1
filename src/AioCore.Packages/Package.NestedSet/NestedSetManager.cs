﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Package.NestedSet
{
    public class NestedSetManager<TDbContext, T, TKey, TNullableKey> :
        INestedSetManager<T, TKey, TNullableKey>
        where T : class, INestedSet<T, TKey, TNullableKey>
        where TDbContext : DbContext
    {
        private readonly DbContext _db;

        private static IQueryable<T> QueryById(IQueryable<T> nodes, TKey id)
        {
            return nodes.Where(PropertyEqualsExpression<TKey>(nameof(INestedSet<T, TKey, TNullableKey>.Id), id));
        }

        private IQueryable<T> GetNodes(TNullableKey rootId)
        {
            return _nodesSet.Where(PropertyEqualsExpression(nameof(INestedSet<T, TKey, TNullableKey>.RootId), rootId));
        }

        private readonly DbSet<T> _nodesSet;

        public NestedSetManager(TDbContext dbContext)
        {
            _db = dbContext;
            _nodesSet = dbContext.Set<T>();
        }

        private static Expression<Func<T, bool>> PropertyEqualsExpression(string propertyName, TKey key)
        {
            return PropertyEqualsExpression<TKey>(propertyName, key);
        }

        private static Expression<Func<T, bool>> PropertyEqualsExpression(string propertyName, TNullableKey key)
        {
            return PropertyEqualsExpression<TNullableKey>(propertyName, key);
        }

        private static Expression<Func<T, bool>> PropertyEqualsExpression<TField>(string propertyName, TField key)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "entity");
            if (string.IsNullOrEmpty(propertyName))
                throw new NotSupportedException();
            return Expression.Lambda<Func<T, bool>>(
                Expression.Equal(Expression.Property(parameterExpression, typeof(T), propertyName), Expression.Convert(Expression.Constant(key), typeof(TField))),
                parameterExpression);
        }

        public IEnumerable<T> Delete(TKey nodeId, bool soft = false)
        {
            var nodeToDelete = GetNode(nodeId);
            var nodeToDeleteLeft = nodeToDelete.Left;
            var difference = nodeToDelete.Right - nodeToDelete.Left + 1;
            var rootId = nodeToDelete.RootId;
            var deleted = GetNodes(rootId).Where(s => s.Left >= nodeToDelete.Left && s.Right <= nodeToDelete.Right).ToList();
            if (soft)
                foreach (var node in deleted)
                    node.Moving = true;
            else
                foreach (var node in deleted)
                    _nodesSet.Remove(node);
            var nodesToUpdate = GetNodes(rootId).Where(s => s.Left > nodeToDelete.Left || s.Right >= nodeToDelete.Left).ToList();
            foreach (var nodeToUpdate in nodesToUpdate.Where(nodeToUpdate => !nodeToUpdate.Moving))
            {
                if (nodeToUpdate.Left >= nodeToDeleteLeft)
                    nodeToUpdate.Left -= difference;
                nodeToUpdate.Right -= difference;
            }
            var minLeft = deleted.Min(s => s.Left) - 1;
            foreach (var deletedNode in deleted)
            {
                deletedNode.Left -= minLeft;
                deletedNode.Right -= minLeft;
                deletedNode.ParentId = default;
            }

            if (soft) return deleted;
            _db.SaveChanges();
            foreach (var deletedSite in deleted)
                deletedSite.Id = default;
            return deleted;
        }

        public void MoveToParent(TKey nodeId, TNullableKey parentId,
            NestedSetInsertMode insertMode)
        {
            Move(nodeId, parentId, default, insertMode);
        }

        public void MoveToSibling(TKey nodeId, TNullableKey siblingId,
            NestedSetInsertMode insertMode)
        {
            Move(nodeId, default, siblingId, insertMode);
        }

        private void Move(TKey nodeId, TNullableKey toParentId, TNullableKey toSiblingId,
            NestedSetInsertMode insertMode)
        {
            var deletedNodes = Delete(nodeId);
            Insert(toParentId, toSiblingId, deletedNodes, insertMode);
        }

        public T InsertRoot(T node,
            NestedSetInsertMode insertMode)
        {
            return Insert(default, default, new[] { node }, insertMode).First();
        }

        public List<T> InsertRoot(IEnumerable<T> nodeTree,
            NestedSetInsertMode insertMode)
        {
            return Insert(default, default, nodeTree, insertMode);
        }

        public T InsertBelow(TNullableKey parentId, T node,
            NestedSetInsertMode insertMode)
        {
            return Insert(parentId, default, new[] { node }, insertMode).First();
        }

        public List<T> InsertBelow(TNullableKey parentId, IEnumerable<T> nodeTree,
            NestedSetInsertMode insertMode)
        {
            return Insert(parentId, default, nodeTree, insertMode);
        }

        public T InsertNextTo(TNullableKey siblingId, T node,
            NestedSetInsertMode insertMode)
        {
            return Insert(default, siblingId, new[] { node }, insertMode).First();
        }

        public T InsertNextTo(TNullableKey siblingId, IEnumerable<T> nodeTree,
            NestedSetInsertMode insertMode)
        {
            return Insert(default, siblingId, nodeTree, insertMode).First();
        }

        private List<T> Insert(TNullableKey parentId, TNullableKey siblingId, IEnumerable<T> nodeTree,
            NestedSetInsertMode insertMode)
        {
            var nodeArray = nodeTree as T[] ?? nodeTree.ToArray();
            var lowestLeft = nodeArray.Min(n => n.Left);
            var highestRight = nodeArray.Max(n => n.Right);
            if (lowestLeft == 0 && highestRight == 0)
            {
                if (nodeArray.Length == 1)
                {
                    var node = nodeArray.Single();
                    node.Left = 1;
                    node.Right = 2;
                    lowestLeft = 1;
                    highestRight = 2;
                }
                else
                {
                    throw new ArgumentException("Node tree must have left right values", nameof(nodeTree));
                }
            }
            var difference = highestRight - lowestLeft;
            var nodeTreeRoot = nodeArray.Single(n => n.Left == lowestLeft);
            T parent = null;
            T sibling = null;
            var isRoot = Equals(parentId, default(TNullableKey)) && Equals(siblingId, default(TNullableKey));
            if (!Equals(parentId, default(TNullableKey)) &&
                insertMode == NestedSetInsertMode.Right)
            {
                parent = GetNode(parentId);
                if (parent == null)
                {
                    throw new ArgumentException($"Unable to find node parent with ID of {parentId}");
                }
                var parent1 = parent;
                var rightMostImmediateChild = GetNodes(parent.RootId)
                    .Where(s => s.Left >= parent1.Left && s.Right <= parent1.Right && s.Level == parent1.Level + 1)
                    .OrderByDescending(s => s.Right)
                    .ToList()
                    .FirstOrDefault(n => !n.Moving)
                    ;
                sibling = rightMostImmediateChild;
                if (sibling != null)
                {
                    siblingId = (TNullableKey)(object)sibling.Id;
                }
            }
            int? siblingLeft = null;
            int? siblingRight = null;
            var rootId = default(TNullableKey);
            if (!Equals(siblingId, default(TNullableKey)))
            {
                sibling ??= GetNode(siblingId);
                siblingLeft = sibling.Left;
                siblingRight = sibling.Right;
                parentId = sibling.ParentId;
                rootId = sibling.RootId;
            }
            int? parentLeft = null;
            if (!Equals(parentId, default(TNullableKey)))
            {
                parent ??= GetNode(parentId);
                parentLeft = parent.Left;
                rootId = parent.RootId;
            }
            var minLevel = nodeArray.Min(n => n.Level);
            foreach (var node in nodeArray)
            {
                node.Level -= minLevel;
                if (parent != null)
                {
                    node.Level += parent.Level + 1;
                }
            }
            var left = 0;
            var right = 0;
            switch (insertMode)
            {
                case NestedSetInsertMode.Left:
                    {
                        IEnumerable<T> nodes;
                        if (sibling != null)
                        {
                            nodes = GetNodes(rootId)
                                .Where(s => s.Left >= siblingLeft || s.Right >= siblingRight).ToList()
                                .Where(n => !n.Moving)
                                .ToList();
                            left = sibling.Left;
                            right = sibling.Left + difference;
                            foreach (var nodeToUpdate in nodes)
                            {
                                if (nodeToUpdate.Left >= siblingLeft)
                                    nodeToUpdate.Left += difference + 1;
                                nodeToUpdate.Right += difference + 1;
                            }
                        }
                        else if (parent != null)
                        {
                            nodes = GetNodes(rootId).Where(s => s.Right >= parentLeft).ToList()
                                .Where(n => !n.Moving)
                                .ToList();
                            left = parent.Left + 1;
                            right = left + difference;
                            foreach (var nodeToUpdate in nodes)
                            {
                                if (nodeToUpdate.Left > parentLeft)
                                    nodeToUpdate.Left += difference + 1;
                                nodeToUpdate.Right += difference + 1;
                            }
                        }
                        else
                        {
                            left = 1;
                            right = 1 + difference;
                        }
                    }
                    break;

                case NestedSetInsertMode.Right:
                    {
                        List<T> nodes;
                        if (sibling != null)
                        {
                            nodes = GetNodes(rootId)
                                .Where(s => s.Left > siblingRight || s.Right > siblingRight)
                                .ToList()
                                .Where(n => !n.Moving)
                                .ToList();
                            left = sibling.Right + 1;
                            right = sibling.Right + 1 + difference;
                            foreach (var nodeToUpdate in nodes)
                            {
                                if (nodeToUpdate.Left > siblingLeft)
                                    nodeToUpdate.Left += difference + 1;
                                nodeToUpdate.Right += difference + 1;
                            }
                        }
                        else if (parent != null)
                        {
                            nodes = GetNodes(rootId)
                                .Where(s => s.Right >= parentLeft).ToList()
                                .Where(n => !n.Moving)
                                .ToList();
                            left = parent.Left + 1;
                            right = left + difference;
                            foreach (var nodeToUpdate in nodes)
                            {
                                if (nodeToUpdate.Left > parentLeft)
                                    nodeToUpdate.Left += difference + 1;
                                nodeToUpdate.Right += difference + 1;
                            }
                        }
                        else
                        {
                            left = 1;
                            right = 1 + difference;
                        }
                    }
                    break;
            }
            var leftChange = left - nodeTreeRoot.Left;
            var rightChange = right - nodeTreeRoot.Right;
            foreach (var node in nodeArray)
            {
                node.Left += leftChange;
                node.Right += rightChange;
            }
            nodeTreeRoot.ParentId = parentId;
            var newNodes = nodeArray.Where(n => !n.Moving).ToList();
            if (newNodes.Any())
            {
                _nodesSet.AddRange(newNodes);
            }
            var movingNodes = nodeArray.Where(n => n.Moving).ToList();
            foreach (var node in movingNodes)
            {
                node.Moving = false;
            }
            _db.SaveChanges();
            if (isRoot)
            {
                nodeTreeRoot.RootId = ToNullableKey(nodeTreeRoot.Id);
                nodeTreeRoot.Root = nodeTreeRoot;
                _db.SaveChanges();
            }
            else if (Equals(rootId, default(TNullableKey)))
            {
                var rootIds = newNodes.Select(n => n.RootId).Distinct().ToArray();
                if (rootIds.Length > 1)
                {
                    throw new ArgumentException("Unable to identify root node ID of node tree as multiple have been supplied.");
                }
                if (Equals(rootId, default(TNullableKey)) &&
                    rootIds.Length == 0 || (rootIds.Length == 1 && Equals(rootIds[0], default(TNullableKey))))
                {
                    rootId = rootIds[0];
                    //nodeTreeRoot.RootId = rootId;//ToNullableKey(GetNodes(rootId).Single(n => n.Left == 1).Id);
                }
            }
            if (!Equals(rootId, default(TNullableKey)))
            {
                foreach (var newNode in newNodes)
                {
                    newNode.RootId = rootId;
                }
                _db.SaveChanges();
            }
            else if (!isRoot)
            {
                throw new Exception("Unable to determine root ID of non-root node");
            }
            foreach (var newNode in newNodes)
            {
                if (newNode == nodeTreeRoot) continue;
                var path = GetPathToNode(newNode, newNodes).Reverse();
                var current = newNode;
                foreach (var ancestor in path)
                {
                    current.ParentId = (TNullableKey)(object)ancestor.Id;
                    current = ancestor;
                }
            }
            _db.SaveChanges();
            return newNodes;
        }

        private static TNullableKey ToNullableKey(TKey id)
        {
            return (TNullableKey)(object)id;
        }

        public IQueryable<T> GetDescendants(TKey nodeId, int? depth = null)
        {
            var node = GetNodeData(nodeId);
            var query = _nodesSet.Where(n => n.Left > node.Left && n.Right < node.Right);
            if (depth.HasValue)
            {
                query = query.Where(n => n.Level <= node.Level + depth.Value);
            }
            return query;
        }

        private NodeData GetNodeData(TKey nodeId)
        {
            var node = QueryById(_nodesSet, nodeId)
                .Select(n => new NodeData { Level = n.Level, Left = n.Left, Right = n.Right, RootId = n.RootId }).Single();
            return node;
        }

        private class NodeData
        {
            public int Level { get; set; }
            public int Left { get; set; }
            public int Right { get; set; }
            public TNullableKey RootId { get; set; }
        }

        public IQueryable<T> GetImmediateChildren(TKey nodeId)
        {
            return _nodesSet.Where(PropertyEqualsExpression(nameof(INestedSet<T, TKey, TNullableKey>.ParentId), (TNullableKey)(object)nodeId));
        }

        public IOrderedEnumerable<T> GetPathToNode(TKey nodeId)
        {
            var node = GetNodeData(nodeId);
            return GetPathToNode(node, GetNodes(node.RootId));
        }

        public IOrderedEnumerable<T> GetPathToNode(T node)
        {
            return GetPathToNode(node, GetNodes(node.RootId));
        }

        public static IOrderedEnumerable<T> GetPathToNode(T node, IEnumerable<T> nodeSet)
        {
            return GetPathToNode(AsNodeData(node), nodeSet);
        }

        private static NodeData AsNodeData(T node)
        {
            return new() { Left = node.Left, Right = node.Right, RootId = node.RootId };
        }

        private static IOrderedEnumerable<T> GetPathToNode(NodeData node, IEnumerable<T> nodeSet)
        {
            return nodeSet
                    .Where(n => n.Left < node.Left && n.Right > node.Right)
                    .OrderBy(n => n.Left);
        }

        private T GetNode(TNullableKey id)
        {
            return GetNode((TKey)(object)id);
        }

        private T GetNode(TKey id)
        {
            return _nodesSet.Single(PropertyEqualsExpression(nameof(INestedSet<T, TKey, TNullableKey>.Id), id));
        }
    }
}