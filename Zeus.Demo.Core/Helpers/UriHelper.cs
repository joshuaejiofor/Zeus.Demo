using Zeus.Demo.Core.Helpers.Interfaces;
using Zeus.Demo.Core.Requests.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace Zeus.Demo.Core.Helpers
{
    public class UriHelper(string baseUri) : IUriHelper
    {
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
