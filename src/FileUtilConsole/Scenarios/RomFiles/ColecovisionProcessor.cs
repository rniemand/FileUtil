using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;

namespace FileUtilConsole.Scenarios.RomFiles;

class ColecovisionProcessor
{
  private const string OutputDir = @"\\192.168.0.60\Games\Roms\Colecovision";

  public static void IngestRomFiles(IServiceProvider serviceProvider)
  {
    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = AppConstants.RomWorkingDir,
        OutputDir = OutputDir,
        FileExtension = ".col",
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
        FileExtensions = new[] { ".col" },
        FileNamePattern = "{fileName}.zip",
        RecurseDirs = true,
        DeleteTargetFileIfExists = true,
        DeleteOnSuccess = true
      });
  }
}
