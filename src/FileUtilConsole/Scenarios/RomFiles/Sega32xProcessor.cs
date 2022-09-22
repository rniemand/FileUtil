using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

namespace FileUtilConsole.Scenarios.RomFiles;

class Sega32xProcessor
{
  private const string OutputDir = @"\\192.168.0.60\Games\Roms\Sega-32x";

  public static void IngestRomFiles(IServiceProvider serviceProvider)
  {
    var logger = serviceProvider.GetRequiredService<ILoggerAdapter<Sega32xProcessor>>();
    logger.LogInformation("Starting to ingest ROM files");

    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".32x",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".zip",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".7z",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      });

    logger.LogInformation("All done!");
  }

  public static void ZipIngestedRomFiles(IServiceProvider serviceProvider)
  {
    var logger = serviceProvider.GetRequiredService<ILoggerAdapter<Sega32xProcessor>>();
    logger.LogInformation("Starting to ZIP ROM files");

    serviceProvider
      .GetRequiredService<IFileZipper>()
      .Run(new FileZipperConfig
      {
        SourceDir = OutputDir,
        FileExtensions = new[] { ".32x" },
        FileNamePattern = "{fileName}.zip",
        RecurseDirs = true,
        DeleteTargetFileIfExists = true,
        DeleteOnSuccess = true
      });

    logger.LogInformation("All done.");
  }
}
