﻿using System.Runtime.Serialization;

namespace BNManager.Enums;

/// <summary>
/// The current state of a beatmap nominator.
/// </summary>
internal enum AskState
{
  /// <summary>
  /// Represents all/no specific ask states.
  /// </summary>
  None,

  /// <summary>
  /// The nominator has declined to nominate the beatmap.
  /// </summary>
  [EnumMember(Value = "declined")]
  Declined,

  /// <summary>
  /// The nominator is unlikely to nominate the beatmap, eg. due to specific requirements.
  /// </summary>
  [EnumMember(Value = "unlikely")]
  Unlikely,

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
