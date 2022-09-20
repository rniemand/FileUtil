using FileUtil.Common.Extensions;
using FileUtil.Common.Models.Config;
using FileUtil.Common.Models.Infos;
using RnCore.Logging;

namespace FileUtil.Common.Renamers;

public interface ISimpleFileRenamer
{
  ISimpleFileRenamer Rename(SimpleFileRenamerConfig config);
}

public class SimpleFileRenamer : ISimpleFileRenamer
{
  private readonly ILoggerAdapter<SimpleFileRenamer> _logger;

  public SimpleFileRenamer(ILoggerAdapter<SimpleFileRenamer> logger)
  {
    _logger = logger;
  }


  // Public methods
  public ISimpleFileRenamer Rename(SimpleFileRenamerConfig config)
  {
    var files = GetMatchingFiles(config);
    if (files.Count == 0)
      return this;

    _logger.LogInformation("Processing {count} files", files.Count);
    foreach (var file in files)
    {
      var fileInfo = new BasicFileInfo(file);
      var targetPath = Path.Join(config.OutputDir, GenerateFilePath(fileInfo, config.FileNamePattern));

      var targetDir = Path.GetDirectoryName(targetPath);
      if (string.IsNullOrWhiteSpace(targetDir))
        throw new Exception($"Unable to calculate target dir: {targetPath}");

      if (!Directory.Exists(targetDir))
        Directory.CreateDirectory(targetDir);

      if (File.Exists(targetPath))
        File.Delete(targetPath);

      _logger.LogDebug("Moving file:\n\t{source} =>\n\t{dest}", file.FullName, targetPath);
      File.Move(file.FullName, targetPath);
    }

    _logger.LogInformation("All done.");
    return this;
  }


  // Internal methods
  private static List<FileInfo> GetMatchingFiles(SimpleFileRenamerConfig config)
  {
    var directoryInfo = new DirectoryInfo(config.SourceDir);

    return directoryInfo.GetFiles("*.*", SearchOption.AllDirectories)
      .Where(x => x.Extension.IgnoreCaseEquals(config.FileExtension))
      .ToList();
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
