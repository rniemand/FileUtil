namespace FileUtil.Common.Models.Config;

public class FileZipperConfig
{
  public string[] FileExtensions { get; set; } = Array.Empty<string>();
  public string SourceDir { get; set; } = string.Empty;
  public string FileNamePattern { get; set; } = string.Empty;
  public bool RecurseDirs { get; set; } = true;
  public bool DeleteTargetFileIfExists { get; set; }
  public bool DeleteOnSuccess { get; set; } = true;
}
