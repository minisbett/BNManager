using System.Runtime.Serialization;

namespace BNManager.Enums;

/// <summary>
/// An enum containing all types of a Beatmap Nominator (On probation, Full BN or NAT/Evaluator).
/// </summary>
internal enum Group
{
  /// <summary>
  /// A Beatmap Nominator on probation.
  /// </summary>
  [EnumMember(Value = "probation")]
  Probation,

  /// <summary>
  /// A full Beatmap Nominator.
  /// </summary>
  [EnumMember(Value = "full")]
  Full,

  /// <summary>
  /// A Nomination Assessment Team member.
  /// </summary>
  [EnumMember(Value = "evaluator")]
  NAT,
}