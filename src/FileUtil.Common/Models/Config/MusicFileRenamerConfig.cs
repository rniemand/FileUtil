namespace FileUtil.Common.Models.Config;

public class MusicFileRenamerConfig
{
  public string SourceDir { get; set; } = string.Empty;
  public string OutputDir { get; set; } = string.Empty;
  public string ProblemDir { get; set; } = string.Empty;
  public string FileNamePattern { get; set; } = string.Empty;
  public bool MoveProblematicFiles { get; set; }


  // Calculated parts
  public string[] ProblemDirParts { get; set; } = Array.Empty<string>();
  public string[] OutputDirParts { get; set; } = Array.Empty<string>();
  public string[] SourceDirParts { get; set; } = Array.Empty<string>();
}
