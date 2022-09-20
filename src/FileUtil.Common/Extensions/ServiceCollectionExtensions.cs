using FileUtil.Common.Renamers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RnCore.Logging;

namespace FileUtil.Common.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddLoggingAndConfiguration(this IServiceCollection services, IConfiguration configuration)
  {
    services.TryAddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

    return services
      // Configuration
      .AddSingleton<IConfiguration>(configuration)

      // Logging
      .AddLogging(loggingBuilder =>
      {
        // configure Logging with NLog
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(configuration);
      });
  }

  public static IServiceCollection AddLoggingAndConfiguration(this IServiceCollection services) =>
    services.AddLoggingAndConfiguration(new ConfigurationBuilder()
      .AddJsonFile("FileUtils.json", optional: true)
      .Build());

  public static IServiceCollection AddFileUtils(this IServiceCollection services)
  {
    return services
      .AddSingleton<IMusicFileRenamer, MusicFileRenamer>()
      .AddSingleton<ISimpleFileRenamer, SimpleFileRenamer>()
      .AddSingleton<IFileZipper, FileZipper>();
  }
}
