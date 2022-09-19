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

    if (UpperAlphaRxp.IsMatch(first))
      return first;

    throw new Exception($"Need to add support for this... {first}");
  }

  public static string PadLeftInt(this int value, int padding, char paddingChar = '0') =>
    value.ToString("D").PadLeft(padding, paddingChar);
}
