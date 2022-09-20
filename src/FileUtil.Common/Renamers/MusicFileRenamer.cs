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
  private readonly HashSet<string> _formats = new() { ".mp3", ".m4a", ".mp4" };

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
    var musicFiles = GetMusicFiles(config);

    if(musicFiles.Count == 0)
      return;

    var counter = 1;
    _logger.LogInformation("Processing {count} music files", musicFiles.Count);

    foreach (var mediaFile in musicFiles)
    {
      var baseMessage = $"\rProcessing {counter++} of {musicFiles.Count} file(s)";
      //_logger.LogTrace("Processing {count} of {total} file(s)", counter++, musicFiles.Count);
      var mp3Info = Mp3RenameHelper.ExtractMp3FileInfo(mediaFile, new MediaInfoWrapper(mediaFile.FullName), config.SourceDir);

      if (!mp3Info.IsValid())
      {
        Console.Write($"{baseMessage} - BAD ");
        HandleErroneousFile(config, mediaFile);
      }
      else
      {
        Console.Write($"{baseMessage} - GOOD");
        ProcessFileRename(config, mp3Info, mediaFile);
      }
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
      //_logger.LogWarning("Skipping file: {file}", mediaFile.FullName);
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

    //_logger.LogDebug("Moving problem file: \n\t{original} => \n\t{new}", mediaFile.FullName, errorPath);
    File.Move(mediaFile.FullName, errorPath);
  }

  private void ProcessFileRename(MusicFileRenamerConfig config, Mp3FileInfo songInfo, FileSystemInfo fileInfo)
  {
    var targetFileName = Path.Join(config.OutputDir,
      GenerateFileName(songInfo, config.FileNamePattern));

    var targetDir = Path.GetDirectoryName(targetFileName);
    
    if (string.IsNullOrWhiteSpace(targetDir))
      throw new Exception($"Unable to work out target dir for: {targetFileName}");

    if (!Directory.Exists(targetDir))
      Directory.CreateDirectory(targetDir);

    //_logger.LogInformation("Renaming:\n\t{old} => \n\t{new}", fileInfo.FullName, targetFileName);

    if (File.Exists(targetFileName))
      File.Delete(targetFileName);

    File.Move(fileInfo.FullName, targetFileName);
  }

  private static string GenerateFileName(Mp3FileInfo info, string template)
  {
    return template
      .Replace("{ext}", info.FileExtension)
      .Replace("{albumTitle}", info.AlbumTitle)
      .Replace("{songTitle}", info.SongTitle)
      .Replace("{artist}", info.Artist)
      .Replace("{aDirLetter}", info.ArtistDirLetter)
      .Replace("{songNumber}", info.SongPosition.PadLeftInt(2))
      .Replace("{songNumber3}", info.SongPosition.PadLeftInt(3))
      .Replace("{songYear}", info.SongDate.Year.ToString("D"))
      .ToSafeFilePath();
  }
}
