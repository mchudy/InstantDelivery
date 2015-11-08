using InstantDelivery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InstantDelivery.Core.Extensions
{
    /// <summary>
    /// Metody rozszerzające klasy IQuerable
    /// </summary>
    public static class IQueryableExtensions
    {
        public static IList<T> Page<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : Entity
        {
            IQueryable<T> result = source;
            if (!source.IsOrdered())
            {
                result = source.OrderBy(e => e.Id);
            }
            return result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public static bool IsOrdered<T>(this IQueryable<T> queryable)
        {
            return OrderingMethodFinder.OrderMethodExists(queryable.Expression);
        }

        private class OrderingMethodFinder : ExpressionVisitor
        {
            bool orderingMethodFound;

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                var name = node.Method.Name;

                if (node.Method.DeclaringType == typeof(Queryable) && (
                    name.StartsWith("OrderBy", StringComparison.Ordinal) ||
                    name.StartsWith("ThenBy", StringComparison.Ordinal)))
                {
                    orderingMethodFound = true;
                }

                return base.VisitMethodCall(node);
            }

            public static bool OrderMethodExists(Expression expression)
            {
                var visitor = new OrderingMethodFinder();
                visitor.Visit(expression);
                return visitor.orderingMethodFound;
            }
        }
    }

}
