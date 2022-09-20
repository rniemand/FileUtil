using System.IO.Compression;
using FileUtil.Common.Extensions;
using FileUtil.Common.Models.Config;
using FileUtil.Common.Models.Infos;
using RnCore.Logging;

namespace FileUtil.Common.Renamers;

public interface IFileZipper
{
  void Run(FileZipperConfig config);
}

public class FileZipper : IFileZipper
{
  private readonly ILoggerAdapter<FileZipper> _logger;

  public FileZipper(ILoggerAdapter<FileZipper> logger)
  {
    _logger = logger;
  }


  // Interface methods
  public void Run(FileZipperConfig config)
  {
    _logger.LogInformation("Scanning '{path}' for files", config.SourceDir);
    var files = GetMatchingFiles(config);
    _logger.LogTrace("Found {count} files", files.Count);

    if (files.Count == 0)
      return;

    var fileNumber = 0;
    _logger.LogInformation("Zipping {count} files in {path}", files.Count, config.SourceDir);
    foreach (var fileInfo in files)
    {
      fileNumber++;
      var initialSize = fileInfo.Length;

      var zippedSize = CreateZipFile(config, new BasicFileInfo(fileInfo));
      if (zippedSize == 0)
        continue;

      var savedSize = initialSize - zippedSize;

      Console.Write($"\rProcessing {fileNumber} of {files.Count} | " +
                    $"{initialSize} -> {zippedSize} ({savedSize} saved)" +
                    "          ");
    }

    _logger.LogInformation("All done!");
  }


  // Internal methods
  private static List<FileInfo> GetMatchingFiles(FileZipperConfig config)
  {
    var directoryInfo = new DirectoryInfo(config.SourceDir);
    var depth = config.RecurseDirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

    return directoryInfo.GetFiles("*.*", depth)
      .Where(x => config.FileExtensions.Any(e => e.IgnoreCaseEquals(x.Extension)))
      .ToList();
  }

  private long CreateZipFile(FileZipperConfig config, BasicFileInfo fileInfo)
  {
    var outDir = Path.GetDirectoryName(fileInfo.FileInfo.FullName);
    var genFileName = GenerateFilePath(fileInfo, config.FileNamePattern);
    var outFileName = Path.Join(outDir, genFileName);

    if (File.Exists(outFileName))
    {
      if (!config.DeleteTargetFileIfExists)
      {
        _logger.LogWarning("Target file '{path}' already exists, DeleteTargetFileIfExists = FALSE", outFileName);
        return 0;
      }

      File.Delete(outFileName);
    }

    using var fileStream = new FileStream(outFileName, FileMode.CreateNew);
    using var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true);
    archive.CreateEntryFromFile(fileInfo.FileInfo.FullName,
      fileInfo.FileInfo.Name,
      CompressionLevel.SmallestSize);

    if (!config.DeleteOnSuccess)
      return GetFileSize(outFileName);

    File.Delete(fileInfo.FileInfo.FullName);
    return GetFileSize(outFileName);
  }

  private static long GetFileSize(string path)
  {
    try
    {
      return (new FileInfo(path)).Length;
    }
    catch
    {
      return 0;
    }
  }

  private static string GenerateFilePath(BasicFileInfo info, string template)
  {
    return template
      .Replace("{fileNameDirLetter}", info.FileNameDirLetter)
      .Replace("{fileName}", info.FileName)
      .Replace("{ext}", info.Extension)
      .ToSafeFilePath();
  }
}
