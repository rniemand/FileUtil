using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FileUtil.Common.Extensions;

public static class StringExtensions
{
  private static Regex UpperAlphaRxp = new("[A-Z]", RegexOptions.Compiled);

  public static string GetFirstDirLetter(this string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      throw new Exception("No value provided");

    var first = value.Trim().ToUpper()[..1];

    if (int.TryParse(first, out var _))
      return "#";

    if (first == "_" || first == "(" || first == "-")
      return "#";

    if (UpperAlphaRxp.IsMatch(first))
      return first;

    throw new Exception($"Need to add support for this... {first}");
  }

  public static string PadLeftInt(this int value, int padding, char paddingChar = '0') =>
    value.ToString("D").PadLeft(padding, paddingChar);

  public static string[] ExtractDirParts(this string path) =>
    path.Split(new[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries);

  public static bool IgnoreCaseEquals(this string src, string compare) =>
    src.Equals(compare, StringComparison.InvariantCultureIgnoreCase);

  public static bool IgnoreCaseContains(this string src, string compare) =>
    src.Contains(compare, StringComparison.InvariantCultureIgnoreCase);

  public static bool IgnoreCaseStartsWith(this string src, string compare) =>
    src.StartsWith(compare, StringComparison.InvariantCultureIgnoreCase);

  public static string ToSafeFilePath(this string filePath)
  {
    var parts = filePath.ExtractDirParts();

    return Path.Join(parts)
      .Replace(",", "")
      .Replace("\"", "")
      .Replace("?", "")
      .Replace(":", "")
      .Replace("<", "")
      .Replace(">", "");
  }
}
