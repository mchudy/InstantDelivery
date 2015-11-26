using System.Collections.Generic;

namespace InstantDelivery.Service.Paging
{
    /// <summary>
    /// Obiekt reprezentujący wynik zapytania o stronę danych
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedResult<TEntity>
    {
        /// <summary>
        /// Lista obiektów dla danej strony
        /// </summary>
        public IList<TEntity> PageCollection { get; set; }

        /// <summary>
        /// Łączna liczba stron
        /// </summary>
        public int PageCount { get; set; }
    }
}