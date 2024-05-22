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
  /// The last datetime at which the ask state was updated. Automatically updated when <see cref="State"/> is set.
  /// </summary>
  [JsonProperty("updated_at")]
  public DateTime UpdatedAt { get; set; }

  /// <summary>
  /// The ask state of the Beatmap Nominator.
  /// </summary>
  [JsonProperty("state")]
  [JsonConverter(typeof(StringEnumConverter))]
  public AskState State { get; set; }

  public NominatorState(int id)
  {
    Id = id;
    UpdatedAt = DateTime.UtcNow;
    State = AskState.NotAsked;
  }
}