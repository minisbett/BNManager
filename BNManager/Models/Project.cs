using BNManager.Enums;
using Newtonsoft.Json;
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
  public int BeatmapSetId { get; }

  /// <summary>
  /// The name of the project.
  /// </summary>
  [JsonProperty("name")]
  public string Name { get; set; }

  /// <summary>
  /// The targetted game modes for the project.
  /// </summary>
  [JsonProperty("modes")]
  public Mode[] Modes { get; set; }

  /// <summary>
  /// The list of all Beatmap Nominator states for this project.
  /// </summary>
  [JsonProperty("states")]
  public List<NominatorState> NominatorStates { get; private set; } = new List<NominatorState>();

  /// <summary>
  /// Creates a new instance of <see cref="Project"/> with the specified beatmap set ID, name and targetted modes.
  /// </summary>
  /// <param name="beatmapSetId">The ID of the beatmap set associated with the project.</param>
  /// <param name="name">The name of the project.</param>
  /// <param name="modes">The modes targetted by the project.</param>
  public Project(int beatmapSetId, string name, Mode[] modes)
  {
    BeatmapSetId = beatmapSetId;
    Name = name;
    Modes = modes;
  }
}
