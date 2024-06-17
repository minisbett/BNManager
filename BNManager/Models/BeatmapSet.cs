using Newtonsoft.Json;

namespace BNManager.Models;

/// <summary>
/// Represents a beatmap set fetched from the Mino API.
/// </summary>
internal class BeatmapSet
{
  /// <summary>
  /// The ID of the beatmap set.
  /// </summary>
  [JsonProperty("id")]
  public int Id { get; private set; }

  /// <summary>
  /// The artist of the beatmap set.
  /// </summary>
  [JsonProperty("artist")]
  public string Artist { get; private set; }

  /// <summary>
  /// The title of the beatmap set.
  /// </summary>
  [JsonProperty("title")]
  public string Title { get; private set; }

  /// <summary>
  /// The beatmaps in the beatmap set.
  /// </summary>
  [JsonProperty("beatmaps")]
  public Beatmap[] Beatmaps { get; private set; }

  /// <summary>
  /// The genre of the beatmap set.
  /// </summary>
  [JsonProperty("genre")]
  public Genre Genre { get; private set; }

  /// <summary>
  /// The language of the beatmap set.
  /// </summary>
  [JsonProperty("language")]
  public Language Language { get; private set; }
}

/// <summary>
/// Represents the genre of a beatmap set.
/// </summary>
public class Genre
{
  [JsonProperty("name")]
  public string Name { get; private set; }
}

/// <summary>
/// Represents the language of a beatmap set.
/// </summary>
public class Language
{
  [JsonProperty("name")]
  public string Name { get; private set; }
}