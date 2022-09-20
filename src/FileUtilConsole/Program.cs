using FileUtil.Common.Extensions;
using FileUtil.Common.Models.Config;
using FileUtil.Common.Renamers;
using FileUtilConsole;
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
 */

// GusDocumentProcessor.Run(serviceProvider);


Console.WriteLine();
Console.WriteLine("=======================================================================");
Console.WriteLine("= Fin.");
Console.WriteLine("=======================================================================");
