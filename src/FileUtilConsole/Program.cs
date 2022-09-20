using FileUtil.Common.Extensions;
using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
  .AddLoggingAndConfiguration()
  .AddFileUtils()
  .BuildServiceProvider();

const string scanDir = @"\\192.168.0.60\Documents\Gus-Documents\[Unsorted]\Music\F";

/*
 * Task List
 *  - fix duplication of file name extensions... (simple renamer)
 *  - Try extract MIDI information from files
 *  - logic to clear up bad spacing in file names
 *  - logic to case file names better
 *  - Add rename transaction log for rollback etc.
 */

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
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\cdg",
    FileExtension = ".cdg",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\sxw",
    FileExtension = ".sxw",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\cda",
    FileExtension = ".cda",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\wav",
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
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\doc-files",
    FileExtension = ".doc",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\doc-files",
    FileExtension = ".rtf",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\doc-files",
    FileExtension = ".txt",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\kar",
    FileExtension = ".kar",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\doc-files",
    FileExtension = ".pdf",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\doc-files",
    FileExtension = ".docx",
    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
  })
  .Rename(new SimpleFileRenamerConfig
  {
    SourceDir = scanDir,
    OutputDir = @"\\192.168.0.60\Documents\Gus-Documents\doc-files",
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

Console.WriteLine();
Console.WriteLine("=======================================================================");
Console.WriteLine("= Fin.");
Console.WriteLine("=======================================================================");
