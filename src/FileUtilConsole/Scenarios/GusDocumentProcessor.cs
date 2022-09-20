using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;

namespace FileUtilConsole.Scenarios;

class GusDocumentProcessor
{
  public static void Run(IServiceProvider serviceProvider)
  {
    const string scanDir = @"\\192.168.0.60\Documents\Gus-Documents\[Unsorted]\_current";

    serviceProvider
      .GetRequiredService<IMusicFileRenamer>()
      .Rename(new MusicFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\Music",
        ProblemDir = @"\\192.168.0.60\Documents\Gus-Documents\_Problems",
        MoveProblematicFiles = true,
        FileNamePattern = "{aDirLetter}\\{artist}\\{albumTitle}\\" +
                          "{songNumber} - {artist} - {songTitle} ({songYear}).{ext}"
      });

    serviceProvider
      .GetRequiredService<ISimpleFileRenamer>()
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\cdg",
        FileExtension = ".cdg",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\sxw",
        FileExtension = ".sxw",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\cda",
        FileExtension = ".cda",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\wav",
        FileExtension = ".wav",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\midi",
        FileExtension = ".mid",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\Documents",
        FileExtension = ".doc",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\rtf",
        FileExtension = ".rtf",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\pps",
        FileExtension = ".pps",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\txt",
        FileExtension = ".txt",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\kar",
        FileExtension = ".kar",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\pdf",
        FileExtension = ".pdf",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\Documents",
        FileExtension = ".docx",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\FileTypes\odt",
        FileExtension = ".odt",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      })
      .Rename(new SimpleFileRenamerConfig
      {
        SourceDir = scanDir,
        OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\E-Books",
        FileExtension = ".epub",
        FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
      });
  }
}
