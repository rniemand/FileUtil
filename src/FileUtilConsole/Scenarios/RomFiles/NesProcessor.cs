using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

namespace FileUtilConsole.Scenarios.RomFiles;

class NesProcessor
{
  private const string OutputDir = @"\\192.168.0.60\Games\Roms\NES";

  public static void IngestRomFiles(IServiceProvider serviceProvider)
  {
    var logger = serviceProvider.GetRequiredService<ILoggerAdapter<NeoGeoProcessor>>();
    logger.LogInformation("Starting to ingest ROM files");

    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".nes",
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
    var logger = serviceProvider.GetRequiredService<ILoggerAdapter<NeoGeoProcessor>>();
    logger.LogInformation("Starting to ZIP ROM files");

    serviceProvider
      .GetRequiredService<IFileZipper>()
      .Run(new FileZipperConfig
      {
        SourceDir = OutputDir,
        FileExtensions = new[] { ".nes" },
        FileNamePattern = "{fileName}.zip",
        RecurseDirs = true,
        DeleteTargetFileIfExists = true,
        DeleteOnSuccess = true
      });

    logger.LogInformation("All done.");
  }
}
