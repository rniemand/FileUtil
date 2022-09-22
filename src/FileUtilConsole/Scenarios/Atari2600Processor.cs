using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

namespace FileUtilConsole.Scenarios;

class Atari2600Processor
{
  private const string OutputDir = @"\\192.168.0.60\Games\Roms\Atari 2600";

  public static void IngestRomFiles(IServiceProvider serviceProvider)
  {
    var logger = serviceProvider.GetRequiredService<ILoggerAdapter<Atari2600Processor>>();

    logger.LogInformation("Starting to ingest ROM files");

    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".bin",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".zip",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      });

    logger.LogInformation("All done!");
  }

  public static void ZipIngestedRomFiles(IServiceProvider serviceProvider)
  {
    serviceProvider
      .GetRequiredService<IFileZipper>()
      .Run(new FileZipperConfig
      {
        SourceDir = OutputDir,
        FileExtensions = new[] { ".bin" },
        FileNamePattern = "{fileName}.zip",
        RecurseDirs = true,
        DeleteTargetFileIfExists = true,
        DeleteOnSuccess = true
      });
  }
}
