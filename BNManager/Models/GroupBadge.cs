using BNManager.Enums;
using Microsoft.UI.Xaml.Media;
using System;
using System.Linq;
using Windows.UI;

namespace BNManager.Models;

/// <summary>
/// Represents a group badge of a Beatmap Nominator.
/// </summary>
internal class GroupBadge
{
  /// <summary>
  /// The group of the Beatmap Nominator in the respective modes.
  /// </summary>
  private readonly Group _group;

  /// <summary>
  /// The modes in which the group applies.
  /// </summary>
  private readonly Mode[] _modes;

  /// <summary>
  /// The display name of the group.
  /// </summary>
  public string DisplayName => _group switch
  {
    Group.Probation => "Beatmap Nominator (Probation)",
    Group.Full => "Beatmap Nominator",
    Group.NAT => "Nomination Assessment Team",
    _ => throw new ArgumentOutOfRangeException(nameof(_group), _group, null)
  };

  /// <summary>
  /// The text color of the group name.
  /// </summary>
  public Brush Brush => new SolidColorBrush(_group switch
  {
    Group.Probation => Color.FromArgb(255, 199, 139, 201),
    Group.Full => Color.FromArgb(255, 163, 71, 235),
    Group.NAT => Color.FromArgb(255, 250, 55, 3),
    _ => throw new ArgumentOutOfRangeException(nameof(_group), _group, null)
  });

  /// <summary>
  /// A string containing the mode icons (special font) for the modes the group applies to.
  /// </summary>
  public string ModeIcons => string.Join(" ", _modes.Select(mode => mode switch
  {
    Mode.None => '\uE805', // osu
    Mode.Standard => '\uE800', // mode-osu
    Mode.Taiko => '\uE803', // mode-taiko
    Mode.Catch => '\uE801', // mode-ctb
    Mode.Mania => '\uE802', // mode-mania
    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
  }));

  public GroupBadge(Group group, Mode[] modes)
  {
    _group = group;
    _modes = modes.OrderBy(mode => mode).ToArray();
  }
}