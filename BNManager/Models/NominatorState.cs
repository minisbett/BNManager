using BNManager.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace BNManager.Models;

/// <summary>
/// Represents the current state of a Beatmap Nominator (Ask state, when it was last updated, osu! user ID of BN).
/// </summary>
internal class NominatorState
{
  /// <summary>
  /// The osu! user ID of the Beatmap Nominator.
  /// </summary>
  [JsonProperty("id")]
  public int Id { get; private set; }

  /// <summary>
  /// The osu! name of the Beatmap Nominator, fetched from the Mappers' Guild API and cached for display purposes.
  /// </summary>
  [JsonProperty("name")]
  public string Name { get; set; }

  /// <summary>
  /// The last datetime at which the ask state was updated. Automatically updated when <see cref="AskState"/> is set.
  /// </summary>
  [JsonProperty("updated_at")]
  public DateTime UpdatedAt { get; set; }

  /// <summary>
  /// The ask state of the Beatmap Nominator.
  /// </summary>
  [JsonProperty("state")]
  [JsonConverter(typeof(StringEnumConverter))]
  public AskState AskState { get; set; }

  /// <summary>
  /// Notes about the state of the nominator.
  /// </summary>
  [JsonProperty("notes")]
  public string Notes { get; set; }

  [JsonConstructor]
  private NominatorState() { }

  /// <summary>
  /// Creates a new <see cref="NominatorState"/> for the specified nominator.
  /// </summary>
  /// <param name="nominator"></param>
  public NominatorState(Nominator nominator)
  {
    Id = nominator.Id;
    Name = nominator.Name;
    UpdatedAt = DateTime.UtcNow;
    AskState = AskState.NotAsked;
  }
}