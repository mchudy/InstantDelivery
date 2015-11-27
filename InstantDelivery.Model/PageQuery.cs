using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace InstantDelivery.Model
{
    /// <summary>
    /// Obiekt reprezentuj¹cy zapytanie o stronê danych
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PageQuery<TEntity>
    {
        /// <summary>
        /// Rozmiar strony
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Numer strony
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Filtry danych
        /// </summary>
        public IList<Expression<Func<TEntity, bool>>> Filters { get; } =
            new List<Expression<Func<TEntity, bool>>>();

        /// <summary>
        /// W³aœciwoœæ, po której dane maj¹ byæ sortowane
        /// </summary>
        public string SortProperty { get; set; }

        /// <summary>
        /// Kierunek sortowania
        /// </summary>
        public ListSortDirection? SortDirection { get; set; }
    }
}