using BNManager.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BNManager.Models;

/// <summary>
/// Represents a beatmap set (project) with general info and all it's Beatmap Nominator states.
/// </summary>
internal class Project
{
  /// <summary>
  /// The ID of the beatmap set.
  /// </summary>
  [JsonProperty("beatmapset_id")]
  public int BeatmapSetId { get; private set; }

  /// <summary>
  /// The name of the project.
  /// </summary>
  [JsonProperty("name")]
  public string Name { get; set; } = "New Project";

  /// <summary>
  /// The targetted game modes for the project.
  /// </summary>
  [JsonProperty("modes")]
  public Mode[] Modes { get; set; } = Enum.GetValues<Mode>();

  /// <summary>
  /// The genre of the beatmap set in the project.
  /// </summary>
  public string Genre { get; set; } = "Unspecified";

  /// <summary>
  /// The language of the beatmap set in the project.
  /// </summary>
  public string Language { get; set; } = "Unspecified";

  /// <summary>
  /// The list of all Beatmap Nominator states for this project.
  /// </summary>
  [JsonProperty("states")]
  public List<NominatorState> NominatorStates { get; private set; } = new List<NominatorState>();

  [JsonConstructor]
  private Project() { }

  /// <summary>
  /// Creates a new instance of <see cref="Project"/> with the specified beatmap set ID, name and targetted modes.
  /// </summary>
  /// <param name="beatmapSetId">The ID of the beatmap set associated with the project.</param>
  /// <param name="name">The name of the project.</param>
  /// <param name="genre">The genre of the beatmap set associated with the project.</param>
  /// <param name="language">The language of the beatmap set associated with the project.</param>
  /// <param name="modes">The modes targetted by the project.</param>
  public Project(int beatmapSetId, string name, Mode[] modes, string genre, string language)
  {
    BeatmapSetId = beatmapSetId;
    Name = name;
    Modes = modes;
    Genre = genre;
    Language = language;
  }
}
