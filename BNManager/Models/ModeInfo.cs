using BNManager.Enums;
using Newtonsoft.Json;

namespace BNManager.Models;

/// <summary>
/// Represents the group of a Beatmap Nominator (On probation, Full BN or NAT/Evaluator) in a specific mode.
/// </summary>
internal class ModeInfo
{
  /// <summary>
  /// The mode the group applies to.
  /// </summary>
  [JsonProperty("mode")]
  public Mode Mode { get; private set; }

  /// <summary>
  /// The group of the Beatmap Nominator.
  /// </summary>
  [JsonProperty("level")]
  public Group Group { get; private set; }
}
