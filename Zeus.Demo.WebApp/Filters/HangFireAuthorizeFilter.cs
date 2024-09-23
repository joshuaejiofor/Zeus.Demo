using Hangfire.Dashboard;

namespace Zeus.Demo.WebApp.Filters
{
    public class HangFireAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();

            // Allow authenticated admin users to see the Dashboard
            //return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole(UserRole.Admin.ToString());

            return true;

        }
    }
}