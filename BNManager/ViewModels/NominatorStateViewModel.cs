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
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(AskState))]
  [NotifyPropertyChangedFor(nameof(AvatarUrl))]
  [NotifyPropertyChangedFor(nameof(IsOpened))]
  [NotifyPropertyChangedFor(nameof(GroupBadges))]
  [NotifyPropertyChangedFor(nameof(Nominator))]
  private NominatorState _state;

  /// <summary>
  /// The ask state of the nominator.
  /// </summary>
  public AskStateViewModel AskState
  {
    get => AskStateViewModel.Options.FirstOrDefault(x => x.State == State?.AskState);
    set
    {
      if (value is null)
        return;


      State.AskState = value.State;
      ProjectService.Save();
    }
  }

  /// <summary>
  /// A URL to the avatar of the nominator.
  /// </summary>
  public string AvatarUrl => $"https://a.ppy.sh/{State?.Id}";

  /// <summary>
  /// Bool whether the queue of the nominator is opened.
  /// </summary>
  public bool IsOpened => !Nominator?.RequestStatus.Contains(RequestStatus.Closed) ?? false;

  /// <summary>
  /// The group badges of the nominator.
  /// </summary>
  public GroupBadgeViewModel[] GroupBadges =>
    Nominator?.ModesInfo.GroupBy(x => x.Group)
    .Select(x => new GroupBadgeViewModel(x.Key, x.Select(y => y.Mode).ToArray()))
    .ToArray();

  /// <summary>
  /// The nominator this state is representing.
  /// </summary>
  public Nominator Nominator => MappersGuildService.Nominators.FirstOrDefault(x => x.Id == State?.Id);
}