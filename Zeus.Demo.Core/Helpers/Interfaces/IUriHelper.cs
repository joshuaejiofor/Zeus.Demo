using Zeus.Demo.Core.Requests.Filters;

namespace Zeus.Demo.Core.Helpers.Interfaces
{
    public interface IUriHelper
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
