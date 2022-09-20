using FileUtil.Common.Extensions;
using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
  .AddLoggingAndConfiguration()
  .AddFileUtils()
  .BuildServiceProvider();

//serviceProvider
//  .GetRequiredService<IMusicFileRenamer>()
//  .Rename(new MusicFileRenamerConfig
//  {
//    SourceDir = @"D:\Test-Data\Music",
//    OutputDir = @"D:\Test-Data\Renamed",
//    ProblemDir = @"D:\Test-Data\Problems",
//    MoveProblematicFiles = true,
//    FileNamePattern = "{aDirLetter}\\{artist}\\{albumTitle}\\" +
//                      "{songNumber} - {artist} - {songTitle} ({songYear}).{ext}"
//  });

//serviceProvider
//  .GetRequiredService<ISimpleFileRenamer>()
//  .Rename(new SimpleFileRenamerConfig
//  {
//    SourceDir = @"D:\Test-Data\Music",
//    OutputDir = @"D:\Test-Data\Renamed\Midi",
//    FileExtension = ".mid",
//    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
//  });

//serviceProvider
//  .GetRequiredService<ISimpleFileRenamer>()
//  .Rename(new SimpleFileRenamerConfig
//  {
//    SourceDir = @"D:\Test-Data\Music",
//    OutputDir = @"D:\Test-Data\Renamed\wav",
//    FileExtension = ".wav",
//    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
//  });

//serviceProvider
//  .GetRequiredService<ISimpleFileRenamer>()
//  .Rename(new SimpleFileRenamerConfig
//  {
//    SourceDir = @"D:\Test-Data\Music",
//    OutputDir = @"D:\Test-Data\Renamed\cdg",
//    FileExtension = ".cdg",
//    FileNamePattern = "{fileNameDirLetter}\\{fileName}.{ext}"
//  });

Console.WriteLine();
Console.WriteLine();
