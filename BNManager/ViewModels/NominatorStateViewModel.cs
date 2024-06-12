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
  private readonly NominatorState _state;

  /// <summary>
  /// The ask state of the nominator.
  /// </summary>
  public AskStateViewModel AskState
  {
    get => AskStateViewModel.Options.FirstOrDefault(x => x.State == _state.AskState);
    set
    {
      if (_state.AskState == value.State)
        return;

      _state.AskState = value.State;
      OnPropertyChanged(nameof(AskState));
      ProjectService.Save();
    }
  }

  /// <summary>
  /// A URL to the avatar of the nominator.
  /// </summary>
  public string AvatarUrl => $"https://a.ppy.sh/{_state.Id}";

  /// <summary>
  /// Bool whether the queue of the nominator is opened.
  /// </summary>
  public bool IsOpened => !Nominator.RequestStatus.Contains(RequestStatus.Closed);

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
  public Nominator Nominator => MappersGuildService.Nominators.FirstOrDefault(x => x.Id == _state.Id);

  /// <summary>
  /// Creates a new instance of <see cref="NominatorStateViewModel"/> representing the specified nominator state.
  /// </summary>
  /// <param name="state">The nominator state represented.</param>
  public NominatorStateViewModel(NominatorState state)
  {
    _state = state;
  }
}