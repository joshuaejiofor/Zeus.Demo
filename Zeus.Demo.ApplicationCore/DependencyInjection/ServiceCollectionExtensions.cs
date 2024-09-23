using Zeus.Demo.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Zeus.Demo.ApplicationCore.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, bool useSingleton = false)
        {
            var types = new List<Type> { typeof(ServiceBase) };
            var applicationAssembly = Assembly.Load("Zeus.Demo.ApplicationCore");
            applicationAssembly.GetTypes().Where(item => item.BaseType != null && types.Contains(item.BaseType))
                               .ToList().ForEach(assignedTypes =>
                               {
                                   if (useSingleton)
                                       services.AddSingleton(assignedTypes.GetInterfaces()[0], assignedTypes);
                                   else
                                       services.AddScoped(assignedTypes.GetInterfaces()[0], assignedTypes);
                               });

            return services;
        }
    }
}
