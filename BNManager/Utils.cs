using BNManager.Enums;
using System;

namespace BNManager;

/// <summary>
/// A static utility class for utility & extension methods.
/// </summary>
internal static class Utils
{
  /// <summary>
  /// Returns a sanitized version of the given string.<br/>
  /// - Removes all double spaces.<br/>
  /// - Removes leading and trailing whitespaces.
  /// </summary>
  /// <param name="str">The string.</param>
  /// <returns>The sanitized string.</returns>
  public static string GetSanitizedString(string str)
  {
    // Remove all double spaces.
    while (str.Contains("  "))
      str = str.Replace("  ", " ");

    // Remove leading and trailing whitespaces.
    return str.Trim();
  }

  /// <summary>
  /// Returns the mode icon/glyph in the osu extra font of the given mode.
  /// </summary>
  /// <param name="mode">The mode.</param>
  /// <returns>The mode icon.</returns>
  public static string GetModeIcon(this Mode mode) => mode switch
  {
    Mode.None => "\uE805", // osu
    Mode.Standard => "\uE800", // mode-osu
    Mode.Taiko => "\uE803", // mode-taiko
    Mode.Catch => "\uE801", // mode-ctb
    Mode.Mania => "\uE802", // mode-mania
    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
  };

  /// <summary>
  /// Returns the mode name (eg. "osu!std") of the given mode.
  /// </summary>
  /// <param name="mode">The mode.</param>
  /// <returns>The mode name.</returns>
  public static string GetName(this Mode mode) => mode switch
  {
    Mode.None => "osu!",
    Mode.Standard => "osu!std",
    Mode.Taiko => "osu!taiko",
    Mode.Catch => "osu!catch",
    Mode.Mania => "osu!mania",
    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
  };
}