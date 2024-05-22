using ABI.Windows.ApplicationModel.Activation;
using BNManager.Enums;
using BNManager.Models;
using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// Represents a nominator (state) in the project view.
/// </summary>
internal partial class NominatorStateViewModel : ObservableObject
{
  /// <summary>
  /// The backing nominator state.
  /// </summary>
  private readonly NominatorState _nominatorState;

  /// <summary>
  /// The ask state of the nominator.
  /// </summary>
  public DisplayableAskState State
  {
    get => DisplayableAskState.Options.First(x => x.State == _nominatorState.State);
    set
    {
      if (value is null)
        return;

      _nominatorState.State = value.State;
      ProjectService.Save();
    }
  }

  /// <summary>
  /// A URL to the avatar of the nominator.
  /// </summary>
  public string AvatarUrl => $"https://a.ppy.sh/{_nominatorState.Id}";

  /// <summary>
  /// A URL to the profile of the nominator.
  /// </summary>
  public string ProfileUrl => $"https://osu.ppy.sh/users/{_nominatorState.Id}";

  /// <summary>
  /// Bool whether the queue of the nominator is opened.
  /// </summary>
  public bool IsOpened => !Nominator.RequestStatus.Contains(RequestStatus.Closed);

  /// <summary>
  /// The group badges of the nominator.
  /// </summary>
  public GroupBadge[] GroupBadges =>
    Nominator.ModesInfo.GroupBy(x => x.Group)
    .Select(x => new GroupBadge(x.Key, x.Select(y => y.Mode).ToArray()))
    .ToArray();

  /// <summary>
  /// The nominator this state is representing.
  /// </summary>
  public Nominator Nominator => MappersGuildService.Nominators.FirstOrDefault(x => x.Id == _nominatorState.Id);

  public NominatorStateViewModel(NominatorState nominator)
  {
    _nominatorState = nominator;
  }
}