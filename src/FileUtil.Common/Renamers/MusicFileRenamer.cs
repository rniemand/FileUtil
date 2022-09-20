using FileUtil.Common.Extensions;
using FileUtil.Common.Helpers;
using FileUtil.Common.Models.Config;
using FileUtil.Common.Models.Infos;
using MediaInfo;
using RnCore.Logging;

namespace FileUtil.Common.Renamers;

public interface IMusicFileRenamer
{
  void Rename(MusicFileRenamerConfig config);
}

public class MusicFileRenamer : IMusicFileRenamer
{
  private readonly ILoggerAdapter<MusicFileRenamer> _logger;
  private readonly HashSet<string> _formats = new() { ".mp3", ".m4a" };

  public MusicFileRenamer(ILoggerAdapter<MusicFileRenamer> logger)
  {
    _logger = logger;
  }


  // Interface methods
  public void Rename(MusicFileRenamerConfig config)
  {
    config.ProblemDirParts = config.ProblemDir.ExtractDirParts();
    config.OutputDirParts = config.OutputDir.ExtractDirParts();
    config.SourceDirParts = config.SourceDir.ExtractDirParts();

    foreach (var mediaFile in GetMusicFiles(config))
    {
      var mp3Info = Mp3RenameHelper.ExtractMp3FileInfo(mediaFile, new MediaInfoWrapper(mediaFile.FullName), config.SourceDir);

      if (!mp3Info.Success)
        HandleErroneousFile(config, mediaFile);
      else
        ProcessFileRename(config, mp3Info, mediaFile);
    }
  }


  // Internal methods
  private List<FileInfo> GetMusicFiles(MusicFileRenamerConfig config)
  {
    var directoryInfo = new DirectoryInfo(config.SourceDir);

    return directoryInfo.GetFiles("*.*", SearchOption.AllDirectories)
      .Where(x => _formats.Contains(x.Extension.ToLower()))
      .ToList();
  }

  private void HandleErroneousFile(MusicFileRenamerConfig config, FileSystemInfo mediaFile)
  {
    if (!config.MoveProblematicFiles)
    {
      _logger.LogWarning("Skipping file: {file}", mediaFile.FullName);
      return;
    }

    if (string.IsNullOrWhiteSpace(config.ProblemDir))
      throw new Exception("No value provided for 'config.ProblemDir'");

    var errorPath = Path.Join(config.ProblemDir,
      Path.Join(mediaFile.FullName.ExtractDirParts()
      .Skip(config.SourceDirParts.Length)
      .ToArray()));

    var errorDir = Path.GetDirectoryName(errorPath);
    if (string.IsNullOrWhiteSpace(errorDir))
      throw new Exception($"Unable to calculate output dir: {errorPath}");

    if (!Directory.Exists(errorDir))
      Directory.CreateDirectory(errorDir);

    if (File.Exists(errorPath))
      File.Delete(errorPath);

    _logger.LogDebug("Moving problem file: {original} => {new}", mediaFile.FullName, errorPath);
    File.Move(mediaFile.FullName, errorPath);
  }

  private void ProcessFileRename(MusicFileRenamerConfig config, Mp3FileInfo mp3Info, FileSystemInfo fileInfo)
  {
    var targetFileName = Path.Join(config.OutputDir, Mp3RenameHelper.Process(mp3Info, config.FileNamePattern));
    var targetDir = Path.GetDirectoryName(targetFileName);

    if (string.IsNullOrWhiteSpace(targetDir))
      throw new Exception($"Unable to work out target dir for: {targetFileName}");

    if (!Directory.Exists(targetDir))
      Directory.CreateDirectory(targetDir);

    _logger.LogInformation("Renaming: {old} => {new}", fileInfo.FullName, targetFileName);

    if (File.Exists(targetFileName))
      File.Delete(targetFileName);

    File.Move(fileInfo.FullName, targetFileName);
  }
}
