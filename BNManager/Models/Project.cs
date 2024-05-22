using System.Collections.Generic;
using BNManager.Enums;
using Newtonsoft.Json;

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

  public Project(int beatmapSetId, string name, Mode[] modes)
  {
    BeatmapSetId = beatmapSetId;
    Name = name;
    Modes = modes;
  }
}
