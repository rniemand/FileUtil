using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;

namespace FileUtilConsole.Scenarios.RomFiles;

class GameGearProcessor
{
  private const string OutputDir = @"\\192.168.0.60\Games\Roms\GameGear";

  public static void IngestRomFiles(IServiceProvider serviceProvider)
  {
    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".gg",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".zip",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      });
  }

  public static void ZipIngestedRomFiles(IServiceProvider serviceProvider)
  {
    serviceProvider
      .GetRequiredService<IFileZipper>()
      .Run(new FileZipperConfig
      {
        SourceDir = OutputDir,
        FileExtensions = new[] { ".gg" },
        FileNamePattern = "{fileName}.zip",
        RecurseDirs = true,
        DeleteTargetFileIfExists = true,
        DeleteOnSuccess = true
      });
  }
}
