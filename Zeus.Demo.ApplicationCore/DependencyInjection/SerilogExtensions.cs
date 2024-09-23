using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Zeus.Demo.ApplicationCore.DependencyInjection
{
    public static class SerilogExtensions
    {
        public static IHostBuilder AddSerilog(this IHostBuilder host)
        {
            host.UseSerilog((hostContext, services, configuration) =>
            {
                var isDev = hostContext.HostingEnvironment.EnvironmentName.ToLower().Equals("development");
                var logDirectory = Environment.GetEnvironmentVariable("LOG_FILEPATH") ?? Directory.GetCurrentDirectory();
                var configLogLevel = Environment.GetEnvironmentVariable("SERILOG_MINIMUMLEVEL_DEFAULT");

                var logLevel = isDev ? LogEventLevel.Warning :
                                   string.IsNullOrEmpty(configLogLevel) ? LogEventLevel.Error : Enum.Parse<LogEventLevel>(configLogLevel);

                configuration
                .WriteTo.File(new JsonFormatter(), Path.Combine(logDirectory, "Logs", "Application.log"),
                    retainedFileCountLimit: 7, fileSizeLimitBytes: null, rollingInterval: RollingInterval.Day, shared: true,
                    restrictedToMinimumLevel: logLevel)
                .WriteTo.Console(restrictedToMinimumLevel: logLevel, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
            });

            return host;
        }
    }
}
