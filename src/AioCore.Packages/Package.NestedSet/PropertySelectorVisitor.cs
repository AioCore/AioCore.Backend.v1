using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Package.NestedSet
{
    internal class PropertySelectorVisitor : ExpressionVisitor
    {
        private readonly List<PropertyInfo> _properties = new();

        internal PropertySelectorVisitor(Expression exp)
        {
            Visit(exp);
        }

        public PropertyInfo Property => _properties.SingleOrDefault();

        private ICollection<PropertyInfo> Properties => _properties;

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var info = node.Member as PropertyInfo;

            if (info == null)
                throw new InvalidOperationException("Member expression must be properties");

            if (node.Expression != null && node.Expression.NodeType != ExpressionType.Parameter)
                throw new InvalidOperationException("Member expression must be bound to lambda parameter");

            _properties.Add(info);
            return node;
        }

        public static PropertyInfo GetSelectedProperty(Expression exp)
        {
            return new PropertySelectorVisitor(exp).Property;
        }

        public static ICollection<PropertyInfo> GetSelectedProperties(Expression exp)
        {
            return new PropertySelectorVisitor(exp).Properties;
        }

        public sealed override Expression Visit(Expression exp)
        {
            if (exp == null)
                return null;

            switch (exp.NodeType)
            {
                case ExpressionType.New:
                case ExpressionType.MemberAccess:
                case ExpressionType.Lambda:
                    return base.Visit(exp);

                default:
                    throw new NotSupportedException();
            }
        }

        protected override Expression VisitLambda<T>(Expression<T> lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException(nameof(lambda));

            if (lambda.Parameters.Count != 1)
                throw new InvalidOperationException("Lambda Expression must have exactly one parameter");

            var body = Visit(lambda.Body);

            return body != lambda.Body ? Expression.Lambda(lambda.Type, body, lambda.Parameters) : lambda;
        }
    }
}