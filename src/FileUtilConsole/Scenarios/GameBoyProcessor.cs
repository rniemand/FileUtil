using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

namespace FileUtilConsole.Scenarios;

class GameBoyProcessor
{
  private const string IngestDir = @"\\192.168.0.60\Games\Roms\_current";
  private const string OutputDir = @"\\192.168.0.60\Games\Roms\GameBoy";

  public static void IngestRomFiles(IServiceProvider serviceProvider)
  {
    var logger = serviceProvider.GetRequiredService<ILoggerAdapter<GameBoyProcessor>>();
    logger.LogInformation("Starting to ingest ROM files");

    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = IngestDir,
        OutputDir = OutputDir,
        FileExtension = ".gb",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = IngestDir,
        OutputDir = OutputDir,
        FileExtension = ".zip",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      });

    logger.LogInformation("All done!");
  }

  public static void ZipIngestedRomFiles(IServiceProvider serviceProvider)
  {
    var logger = serviceProvider.GetRequiredService<ILoggerAdapter<GameBoyProcessor>>();
    logger.LogInformation("Starting to ZIP ROM files");

    serviceProvider
      .GetRequiredService<IFileZipper>()
      .Run(new FileZipperConfig
      {
        SourceDir = OutputDir,
        FileExtensions = new[] { ".gb" },
        FileNamePattern = "{fileName}.zip",
        RecurseDirs = true,
        DeleteTargetFileIfExists = true,
        DeleteOnSuccess = true
      });

    logger.LogInformation("All done.");
  }
}
