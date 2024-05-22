using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BNManager.Enums;

/// <summary>
/// An enum containing all osu! modes.
/// </summary>
internal enum Mode
{
  /// <summary>
  /// No mode, meaning they're a structural NAT.
  /// </summary>
  [EnumMember(Value = "none")]
  None,

  /// <summary>
  /// osu!standard mode.
  /// </summary>
  [EnumMember(Value = "osu")]
  Standard,

  /// <summary>
  /// osu!taiko mode.
  /// </summary>
  [EnumMember(Value = "taiko")]
  Taiko,

  /// <summary>
  /// osu!fruits mode.
  /// </summary>
  [EnumMember(Value = "fruits")]
  Catch,

  /// <summary>
  /// osu!mania mode.
  /// </summary>
  [EnumMember(Value = "mania")]
  Mania
}