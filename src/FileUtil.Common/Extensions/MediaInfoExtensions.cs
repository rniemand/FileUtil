using MediaInfo;

namespace FileUtil.Common.Extensions;

public static class MediaInfoExtensions
{
  public static DateTime GetMp3SongTime(this MediaInfoWrapper wrapper, FileInfo file)
  {
    if (wrapper.Tags.RecordedDate.HasValue)
      return wrapper.Tags.RecordedDate.Value;

    if (wrapper.Tags.ReleasedDate.HasValue)
      return wrapper.Tags.ReleasedDate.Value;

    if (file.LastWriteTime < file.CreationTime)
      return file.LastWriteTime;

    return file.CreationTime;
  }
}
