using System.Runtime.Serialization;

namespace BNManager.Enums;

/// <summary>
/// An enum representing the status(es) of a Beatmap Nominator's request behavior.
/// </summary>
internal enum RequestStatus
{
  /// <summary>
  /// The Beatmap Nominator receives requests via game chat.
  /// </summary>
  [EnumMember(Value = "gameChat")]
  GameChat,

  /// <summary>
  /// The Beatmap Nominator receives requests via a personal queue.
  /// </summary>
  [EnumMember(Value = "personalQueue")]
  PersonalQueue,

  /// <summary>
  /// The Beatmap Nominator currently does not receive requests.
  /// </summary>
  [EnumMember(Value = "closed")]
  Closed
}
