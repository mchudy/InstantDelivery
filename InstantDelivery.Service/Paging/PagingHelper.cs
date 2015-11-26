using InstantDelivery.Domain.Extensions;
using System;
using System.ComponentModel;
using System.Linq;

namespace InstantDelivery.Service.Paging
{
    public static class PagingHelper
    {
        public static PagedResult<T> GetPagedResult<T>(IQueryable<T> source, PageQuery<T> query)
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
            foreach (var filter in query.Filters)
            {
                source = source.Where(filter);
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
