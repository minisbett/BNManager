using System.Runtime.Serialization;

namespace BNManager.Enums;

/// <summary>
/// The current state of a beatmap nominator.
/// </summary>
internal enum AskState
{
  /// <summary>
  /// The nominator has declined to nominate the beatmap.
  /// </summary>
  [EnumMember(Value = "declined")]
  Declined,

  /// <summary>
  /// The nominator has not been asked to nominate the beatmap.
  /// </summary>
  [EnumMember(Value = "not_asked")]
  NotAsked,

  /// <summary>
  /// The nominator has been asked to nominate the beatmap, but has not responded yet.
  /// </summary>
  [EnumMember(Value = "pending")]
  Pending,

  /// <summary>
  /// The nominator has raised the possibility of nominating the beatmap.
  /// </summary>
  [EnumMember(Value = "possible")]
  Possible,

  /// <summary>
  /// The nominator has confirmed that they will nominate the beatmap.
  /// </summary>
  [EnumMember(Value = "confirmed")]
  Confirmed
}
