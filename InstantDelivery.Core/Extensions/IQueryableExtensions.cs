using InstantDelivery.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace InstantDelivery.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IList<T> Page<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : Entity
        {
            return source.OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
