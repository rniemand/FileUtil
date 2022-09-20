namespace FileUtil.Common.Models.Config;

public class SimpleFileRenamerConfig
{
  public string FileExtension { get; set; } = string.Empty;
  public string SourceDir { get; set; } = string.Empty;
  public string OutputDir { get; set; } = string.Empty;
  public string FileNamePattern { get; set; } = string.Empty;
}
