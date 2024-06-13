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
      // Prevent stack overflows because the OnPropertyChanged method will trigger the setter again.
      if (_state.AskState == value.State)
        return;

      _state.AskState = value.State;
      OnPropertyChanged(nameof(AskState)); // Update AskState bindings for other parts of the UI (eg. info dialog & project list)
      ProjectService.Save();
    }
  }

  /// <summary>
  /// The notes for the state of the nominator.
  /// </summary>
  public string Notes
  {
    get => _state.Notes;
    set
    {
      _state.Notes = value;
      ProjectService.Save();
    }
  }

  /// <summary>
  /// A URL to the avatar of the nominator.
  /// </summary>
  public string AvatarUrl => $"https://a.ppy.sh/{_state.Id}";

  /// <summary>
  /// A URL to the game chat of the nominator.
  /// </summary>
  public string GameChatUrl => $"https://osu.ppy.sh/community/chat/?sendto={_state.Id}";

  /// <summary>
  /// A URL to the personal queue of the nominator. Returns *any* valid URL (eg. GameChatUrl) if the nominator does not have a personal queue.
  /// </summary>
  public string PersonalQueueUrl => Nominator.RequestLink is "" ? GameChatUrl : Nominator.RequestLink;

  /// <summary>
  /// Bool whether the queue of the nominator is opened.
  /// </summary>
  public bool IsClosed => Nominator.RequestStatus.Contains(RequestStatus.Closed);

  /// <summary>
  /// Bool whether the nominator receives requests via a personal queue.
  /// </summary>
  public bool HasPersonalQueue => Nominator.RequestStatus.Contains(RequestStatus.PersonalQueue) && Nominator.RequestLink is not "";

  /// <summary>
  /// Bool whether the nominator receives requests via game chat.
  /// </summary>
  public bool HasGameChatQueue => Nominator.RequestStatus.Contains(RequestStatus.GameChat);

  /// <summary>
  /// The group badges of the nominator.
  /// </summary>
  public GroupBadgeViewModel[] GroupBadges =>
    Nominator?.ModesInfo.GroupBy(x => x.Group)
    .Select(x => new GroupBadgeViewModel(x.Key, x.Select(y => y.Mode).ToArray()))
    .ToArray();

  /// <summary>
  /// Bool whether the nominator has request info specified.
  /// </summary>
  public bool HasRequestInfo => Nominator.RequestInfo is not "";

  /// <summary>
  /// The request info of the nominator, formatted by trimming newlines.
  /// </summary>
  public string RequestInfoFormatted => Nominator.RequestInfo.Trim('\n');

  /// <summary>
  /// The nominator this state is representing.
  /// </summary>
  public Nominator Nominator => MappersGuildService.FromId(_state.Id);

  /// <summary>
  /// Creates a new instance of <see cref="NominatorStateViewModel"/> representing the specified nominator state.
  /// </summary>
  /// <param name="state">The nominator state represented.</param>
  public NominatorStateViewModel(NominatorState state)
  {
    _state = state;
  }
}