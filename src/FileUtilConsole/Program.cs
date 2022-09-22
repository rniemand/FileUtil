using FileUtil.Common.Extensions;
using FileUtilConsole.Scenarios;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
  .AddLoggingAndConfiguration()
  .AddFileUtils()
  .BuildServiceProvider();

/*
 * Task List
 *  - fix duplication of file name extensions... (simple renamer)
 *  - Try extract MIDI information from files
 *  - logic to clear up bad spacing in file names
 *  - logic to case file names better
 *  - Add rename transaction log for rollback etc.
 *  - Add ability to de-dupe a folder (i.e. flatten it)
 *  - Ability to clean up directory based on rules (DB backups)
 *  - Ability to search for file patterns and delete them - e.g. "(J)"
 */

NeoGeoProcessor.IngestRomFiles(serviceProvider);
NeoGeoProcessor.ZipIngestedRomFiles(serviceProvider);


Console.WriteLine();
Console.WriteLine("=======================================================================");
Console.WriteLine("= Fin.");
Console.WriteLine("=======================================================================");
