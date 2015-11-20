using InstantDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace InstantDelivery.Domain.Extensions
{
    /// <summary>
    /// Metody rozszerzające klasy IQuerable
    /// </summary>
    public static class IQueryableExtensions
    {

        private static readonly MethodInfo OrderByMethod =
       typeof(Queryable)
           .GetMethods()
           .Where(method => method.Name == "OrderBy")
           .Single(method => method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
  typeof(Queryable)
      .GetMethods()
      .Where(method => method.Name == "OrderByDescending")
      .Single(method => method.GetParameters().Length == 2);

        //TODO refactor
        public static IQueryable<TSource> OrderByProperty<TSource>
            (this IQueryable<TSource> source, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "posting");
            Expression orderByProperty = Expression.Property(parameter, propertyName);

            LambdaExpression lambda = Expression.Lambda(orderByProperty, parameter);
            MethodInfo genericMethod = OrderByMethod.MakeGenericMethod(
                typeof(TSource), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<TSource>)ret;
        }

        public static IQueryable<TSource> OrderByDescendingProperty<TSource>
    (this IQueryable<TSource> source, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "posting");
            Expression orderByProperty = Expression.Property(parameter, propertyName);

            LambdaExpression lambda = Expression.Lambda(orderByProperty, parameter);
            MethodInfo genericMethod = OrderByDescendingMethod.MakeGenericMethod(
                typeof(TSource), orderByProperty.Type);
            object ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<TSource>)ret;
        }


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
