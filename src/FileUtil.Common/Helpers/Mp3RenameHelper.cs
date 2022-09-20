using System.Text.RegularExpressions;
using FileUtil.Common.Extensions;
using FileUtil.Common.Models.Infos;
using MediaInfo;

namespace FileUtil.Common.Helpers;

public class Mp3RenameHelper
{
  private static Regex[] ExtractPatterns = {
    new Regex("([^\\/]+)\\/([^-]+) - ([^\\.]+)\\.\\w{3,4}", RegexOptions.Compiled)
  };

  public static string Process(Mp3FileInfo info, string template)
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

  public static Mp3FileInfo ExtractMp3FileInfo(FileInfo file, MediaInfoWrapper mi, string rootDir)
  {
    var mp3FileInfo = new Mp3FileInfo
    {
      AlbumTitle = mi.Tags.Album,
      SongTitle = mi.Tags.Title,
      Artist = mi.Tags.Artist,
      SongPosition = mi.Tags.TrackPosition ?? 0,
      SongDate = mi.GetMp3SongTime(file),
      FileExtension = file.Extension.Replace(".", "")
    };

    if (string.IsNullOrWhiteSpace(mi.Tags.Artist))
    {
      var rootParts = rootDir.Split(new[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries);
      var fileFullName = string.Join("/", file.FullName
        .Split(new[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries)
        .Skip(rootParts.Length));

      if (!ExtractWithPattern01(mp3FileInfo, fileFullName))
      {
        mp3FileInfo.Success = false;
        return mp3FileInfo;
      }
    }

    // Dynamically calculated properties
    mp3FileInfo.ArtistDirLetter = mp3FileInfo.Artist.GetFirstDirLetter();

    return mp3FileInfo;
  }

  private static bool ExtractWithPattern01(Mp3FileInfo fileInfo, string relFilePath)
  {
    if (!ExtractPatterns[0].IsMatch(relFilePath))
      return false;

    var match = ExtractPatterns[0].Match(relFilePath);

    fileInfo.AlbumTitle = match.Groups[1].Value;
    fileInfo.Artist = match.Groups[2].Value;
    fileInfo.SongTitle = match.Groups[3].Value;

    return true;
  }
}
