using BNManager.Enums;
using System;

namespace BNManager.Models;

/// <summary>
/// Represents a displayable filter option for the nominator state list.
/// </summary>
internal class DisplayableStateFilterOption
{
  /// <summary>
  /// The state to filter for.
  /// </summary>
  public AskState? State { get; }

  public DisplayableStateFilterOption(AskState? state)
  {
    State = state;
  }

  public override string ToString() => State switch
  {
    null => "All",
    AskState.Declined => "Declined",
    AskState.NotAsked => "Not Asked",
    AskState.Pending => "Pending",
    AskState.Possible => "Possible",
    AskState.Confirmed => "Confirmed",
    _ => throw new ArgumentOutOfRangeException(nameof(State), State, null)
  };

  /// <summary>
  /// An array of options for the nominator ask state filter.
  /// </summary>
  public static DisplayableStateFilterOption[] Options { get; } = new DisplayableStateFilterOption[]
  {
    new(null),
    new(AskState.Declined),
    new(AskState.NotAsked),
    new(AskState.Pending),
    new(AskState.Possible),
    new(AskState.Confirmed),
  };
}