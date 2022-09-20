namespace FileUtil.Common.Models.Infos;

public class Mp3FileInfo
{
  public string AlbumTitle { get; set; } = string.Empty;
  public string SongTitle { get; set; } = string.Empty;
  public string Artist { get; set; } = string.Empty;
  public string ArtistDirLetter { get; set; } = string.Empty;
  public int SongPosition { get; set; }
  public DateTime SongDate { get; set; } = DateTime.MinValue;
  public string FileExtension { get; set; } = string.Empty;
  public bool Success { get; set; } = true;
  public FileInfo FileInfo { get; set; }

  public Mp3FileInfo(FileInfo fi)
  {
    FileInfo = fi;
  }

  public bool IsValid()
  {
    if (!Success)
      return false;

    if (string.IsNullOrWhiteSpace(AlbumTitle))
      return false;

    if (string.IsNullOrWhiteSpace(SongTitle))
      return false;

    if (string.IsNullOrWhiteSpace(Artist))
      return false;

    return true;
  }
}
