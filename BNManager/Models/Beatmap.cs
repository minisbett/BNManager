using BNManager.Enums;
using Newtonsoft.Json;

namespace BNManager.Models;

/// <summary>
/// Represents a beatmap fetched from the Mino API, as part of a <see cref="BeatmapSet"/>.
/// </summary>
internal class Beatmap
{
  /// <summary>
  /// The difficulty name of the beatmap.
  /// </summary>
  [JsonProperty("version")]
  public string Version { get; private set; }

  /// <summary>
  /// The difficulty rating of the beatmap.
  /// </summary>
  [JsonProperty("difficulty_rating")]
  public double DifficultyRating { get; private set; }

  /// <summary>
  /// The mode of the beatmap.
  /// </summary>
  [JsonProperty("mode")]
  public Mode Mode { get; private set; }
}
