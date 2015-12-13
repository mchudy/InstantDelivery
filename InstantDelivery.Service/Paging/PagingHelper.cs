using InstantDelivery.Domain.Extensions;
using InstantDelivery.Model.Paging;
using System;
using System.ComponentModel;
using System.Linq;

namespace InstantDelivery.Service.Paging
{
    public static class PagingHelper
    {
        /// <summary>
        /// Zwraca stronę danych z danej kolekcji.
        /// </summary>
        /// <param name="source">Kolekcja</param>
        /// <param name="query">Filtry</param>
        /// <returns></returns>
        public static PagedResult<T> GetPagedResult<T>(IQueryable<T> source, PageQuery query)
        {
            if (string.IsNullOrEmpty(query.SortProperty))
            {
                source = source.OrderByProperty("Id");
            }
            else if (query.SortDirection == ListSortDirection.Descending)
            {
                source = source.OrderByDescendingProperty(query.SortProperty);
            }
            else
            {
                source = source.OrderByProperty(query.SortProperty);
            }
            var pageCount = (int)Math.Ceiling(source.Count() / (double)query.PageSize);
            return new PagedResult<T>
            {
                PageCount = pageCount,
                PageCollection = source.Page(query.PageIndex, query.PageSize)
            };
        }
    }
}
