using FileUtil.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

var serviceProvider = new ServiceCollection()
  .AddLoggingAndConfiguration()
  .AddFileUtils()
  .BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILoggerAdapter<Program>>();

logger.LogInformation("Hello World");
