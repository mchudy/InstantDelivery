using System.Collections.Generic;

namespace InstantDelivery.Services.Paging
{
    public class PageDTO<TEntity>
    {
        public IList<TEntity> PageCollection { get; set; }
        public int PageCount { get; set; }
    }
}