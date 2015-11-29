using InstantDelivery.Model;
using System.Collections.Specialized;
using System.Web;

namespace InstantDelivery.ViewModel.Extensions
{
    public static class PageQueryExtensions
    {
        public static string ToQueryString(this PageQuery query)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString[nameof(PageQuery.PageSize)] = query.PageSize.ToString();
            queryString[nameof(PageQuery.PageIndex)] = query.PageIndex.ToString();
            queryString[nameof(PageQuery.SortDirection)] = query.SortDirection.ToString();
            queryString[nameof(PageQuery.SortProperty)] = query.SortProperty;

            foreach (var entry in query?.Filters)
            {
                if (!string.IsNullOrEmpty(entry.Value))
                {
                    queryString[entry.Key] = entry.Value;
                }
            }

            return queryString.ToString();
        }
    }
}