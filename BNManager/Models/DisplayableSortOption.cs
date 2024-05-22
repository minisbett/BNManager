using BNManager.Enums;
using System;
using System.Linq;
using Windows.UI;

namespace BNManager.Models;

/// <summary>
/// Represents a displayable sort option for the nominator state list.
/// </summary>
internal class DisplayableSortOption
{
  /// <summary>
  /// The sort option value.
  /// </summary>
  public SortOption Option { get; }

  public DisplayableSortOption(SortOption option)
  {
    Option = option;
  }

  public override string ToString() => Option switch
  {
    SortOption.NameAsc => "Name (A-Z)",
    SortOption.NameDesc => "Name (Z-A)",
    SortOption.LevelAsc => "Group (Prob-NAT)",
    SortOption.LevelDesc => "Group (NAT-Prob)",
    _ => throw new ArgumentOutOfRangeException(nameof(Option), Option, null)
  };

  /// <summary>
  /// An array of options for the nominator ask state combo boxes.
  /// </summary>
  public static DisplayableSortOption[] Options { get; } = Enum.GetValues<SortOption>().Select(x => new DisplayableSortOption(x)).ToArray();
}