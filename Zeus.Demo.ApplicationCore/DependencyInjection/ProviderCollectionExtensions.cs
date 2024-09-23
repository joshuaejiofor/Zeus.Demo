using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Zeus.Demo.ApplicationCore.Providers.Interfaces;

namespace Zeus.Demo.ApplicationCore.DependencyInjection
{
    public static class ProviderCollectionExtensions
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, bool useSingleton = false)
        {
            var iProviders = new List<Type> { typeof(IProductProvider) };
            var applicationAssembly = Assembly.Load("Zeus.Demo.ApplicationCore");

            applicationAssembly.GetTypes()
                .Where(item => item.GetInterfaces().Any(type => iProviders.Contains(type) || type.IsGenericType) && !item.IsInterface)
                .ToList().ForEach(assignedTypes =>
                {
                    if (useSingleton)
                        services.AddSingleton(assignedTypes);
                    else
                        services.AddScoped(assignedTypes);
                });

            return services;
        }
    }
}
