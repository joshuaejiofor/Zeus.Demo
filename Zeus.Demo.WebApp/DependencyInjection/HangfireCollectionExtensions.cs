using Hangfire;
using Hangfire.SqlServer;

namespace Zeus.Demo.WebApp.DependencyInjection
{
    public static class HangfireCollectionExtensions
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration config)
        {
            services.AddHangfire(configuration => configuration
                    .UseSqlServerStorage(config.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(10),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(10),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));

            return services;
        }
    }
}
