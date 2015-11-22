using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace InstantDelivery.Services.Paging
{
    public class PageQuery<TEntity>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IList<Expression<Func<TEntity, bool>>> Filters { get; } =
            new List<Expression<Func<TEntity, bool>>>();
        public string SortProperty { get; set; }
        public ListSortDirection? SortDirection { get; set; }
    }
}