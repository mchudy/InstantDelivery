using System.Collections.Generic;
using System.ComponentModel;

namespace InstantDelivery.Model.Paging
{
    /// <summary>
    /// Obiekt reprezentuj¹cy zapytanie o stronê danych
    /// </summary>
    public class PageQuery
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
        public IDictionary<string, string> Filters { get; } =
            new Dictionary<string, string>();

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