using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zeus.Demo.ApplicationCore.DependencyInjection
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, bool useSingleton = false)
        {
            var applicationAssembly = Assembly.Load("Zeus.Demo.EntityFrameworkCore");
            applicationAssembly.GetTypes()
                               .Where(item => item.GetInterfaces().Length > 1 && item.BaseType != null && item.BaseType.IsGenericType && item.BaseType.Name.Contains("Repository"))
                               .ToList().ForEach(assignedTypes =>
                               {
                                   if (useSingleton)
                                       services.AddSingleton(assignedTypes.GetInterfaces()[1], assignedTypes);
                                   else
                                       services.AddScoped(assignedTypes.GetInterfaces()[1], assignedTypes);
                               });

            return services;
        }
    }
}
