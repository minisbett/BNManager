using Newtonsoft.Json;

namespace BNManager.Models;

/// <summary>
/// Represents the configuration for the application.
/// </summary>
internal class Config
{
  /// <summary>
  /// Bool whether the application is in dark mode.
  /// </summary>
  [JsonProperty("dark_mode")]
  public bool DarkMode { get; set; } = true;

  /// <summary>
  /// The opacity of the beatmap background when a project is selected.
  /// </summary>
  [JsonProperty("background_opacity")]
  public int BackgroundOpacity { get; set; } = 0;
}
