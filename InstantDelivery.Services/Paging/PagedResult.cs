using System.Collections.Generic;

namespace InstantDelivery.Services.Paging
{
    public class PagedResult<TEntity>
    {
        public IList<TEntity> PageCollection { get; set; }
        public int PageCount { get; set; }
    }
}