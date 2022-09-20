using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

namespace FileUtilConsole.Scenarios;

class Nintendo64Processor
{
  private const string IngestDir = @"\\192.168.0.60\Games\Roms\_current";
  private const string OutputDir = @"\\192.168.0.60\Games\Roms\Nintendo64";

  public static void IngestRomFiles(IServiceProvider serviceProvider)
  {
    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = IngestDir,
        OutputDir = OutputDir,
        FileExtension = ".z64",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = IngestDir,
        OutputDir = OutputDir,
        FileExtension = ".zip",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      });
  }

  public static void ZipIngestedRomFiles(IServiceProvider serviceProvider)
  {
    serviceProvider
      .GetRequiredService<ILoggerAdapter<Nintendo64Processor>>()
      .LogInformation("Starting to process files");

    serviceProvider
      .GetRequiredService<IFileZipper>()
      .Run(new FileZipperConfig
      {
        SourceDir = OutputDir,
        FileExtensions = new[] { ".z64" },
        FileNamePattern = "{fileName}.zip",
        RecurseDirs = true,
        DeleteTargetFileIfExists = true,
        DeleteOnSuccess = true
      });
  }
}
