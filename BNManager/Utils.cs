using BNManager.Enums;
using BNManager.Models;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

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
}