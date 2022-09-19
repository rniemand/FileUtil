using FileUtil.Common.Extensions;
using FileUtil.Common.Helpers;
using MediaInfo;
using Microsoft.Extensions.DependencyInjection;
using RnCore.Logging;

var serviceProvider = new ServiceCollection()
  .AddLoggingAndConfiguration()
  .AddFileUtils()
  .BuildServiceProvider();

const string musicRoot = @"D:\Test-Data\Music";
const string outDir = @"D:\Test-Data\Renamed";
const string pattern = "{aDirLetter}\\{artist}\\{albumTitle}\\{songNumber} - {artist} - {songTitle} ({songYear}).{ext}";

var formats = new HashSet<string> { ".mp3", ".m4a" };

var logger = serviceProvider.GetRequiredService<ILoggerAdapter<Program>>();
var directoryInfo = new DirectoryInfo(musicRoot);

var musicFiles = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories)
  .Where(x => formats.Contains(x.Extension))
  .ToList();


foreach (FileInfo mediaFile in musicFiles)
{
  var mp3Info = Mp3RenameHelper.ExtractMp3FileInfo(mediaFile, new MediaInfoWrapper(mediaFile.FullName), musicRoot);

  if (!mp3Info.Success)
  {
    logger.LogWarning("Skipping file: {file}", mediaFile.FullName);
    continue;
  }

  var targetFileName = Path.Join(outDir, Mp3RenameHelper.Process(mp3Info, pattern));
  var targetDir = Path.GetDirectoryName(targetFileName);

  if (string.IsNullOrWhiteSpace(targetDir))
    throw new Exception($"Unable to work out target dir for: {targetFileName}");

  if (!Directory.Exists(targetDir))
    Directory.CreateDirectory(targetDir);

  logger.LogInformation("Renaming: {old} => {new}", mediaFile.FullName, targetFileName);

  if(File.Exists(targetFileName))
    File.Delete(targetFileName);

  File.Move(mediaFile.FullName, targetFileName);
}



Console.WriteLine();
Console.WriteLine();
