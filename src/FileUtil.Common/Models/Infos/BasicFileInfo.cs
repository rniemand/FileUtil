using FileUtil.Common.Extensions;

namespace FileUtil.Common.Models.Infos;

public class BasicFileInfo
{
  public FileInfo FileInfo { get; set; }
  public string FileName { get; set; }
  public string FileNameWithExtension { get; set; }
  public string Extension { get; set; }
  public string FileNameDirLetter { get; set; }

  public BasicFileInfo(FileInfo fi)
  {
    FileInfo = fi;
    FileName = Path.GetFileNameWithoutExtension(fi.Name);
    FileNameWithExtension = fi.Name;
    Extension = fi.Extension.ToLower().Replace(".", "");
    FileNameDirLetter = fi.Name.GetFirstDirLetter();
  }
}
