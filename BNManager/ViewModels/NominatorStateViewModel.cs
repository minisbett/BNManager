using BNManager.Enums;
using BNManager.Models;
using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
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
      _state.UpdatedAt = DateTime.UtcNow;
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
  /// A URL to the profile of the nominator.
  /// </summary>
  public string ProfileUrl => $"https://osu.ppy.sh/u/{_state.Id}";

  /// <summary>
  /// A URL to the game chat of the nominator.
  /// </summary>
  public string GameChatUrl => $"https://osu.ppy.sh/community/chat/?sendto={_state.Id}";

  /// <summary>
  /// A URL to the personal queue of the nominator. Returns *any* valid URL (eg. GameChatUrl) if the nominator does not have a personal queue.
  /// </summary>
  public string PersonalQueueUrl => Nominator.RequestLink is "" ? GameChatUrl : Nominator.RequestLink;

  /// <summary>
  /// Bool whether the queue of the nominator is closed.
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

  #region Preferences

  /// <summary>
  /// The genre preferences of the nominator.
  /// </summary>
  public IEnumerable<PreferenceViewModel> GenrePreferences =>
    Nominator.GenrePreferences.Select(x => new PreferenceViewModel(true, x))
      .Concat(Nominator.GenreNegativePreferences.Select(x => new PreferenceViewModel(false, x)));

  /// <summary>
  /// The language preferences of the nominator.
  /// </summary>
  public IEnumerable<PreferenceViewModel> LanguagePreferences =>
    Nominator.LanguagePreferences.Select(x => new PreferenceViewModel(true, x))
      .Concat(Nominator.LanguageNegativePreferences.Select(x => new PreferenceViewModel(false, x)));

  /// <summary>
  /// The language preferences of the nominator.
  /// </summary>
  public IEnumerable<PreferenceViewModel> MapperPreferences =>
    Nominator.MapperPreferences.Select(x => new PreferenceViewModel(true, x))
      .Concat(Nominator.MapperNegativePreferences.Select(x => new PreferenceViewModel(false, x)));

  /// <summary>
  /// The detail preferences of the nominator.
  /// </summary>
  public IEnumerable<PreferenceViewModel> DetailPreferences =>
    Nominator.DetailPreferences.Select(x => new PreferenceViewModel(true, x))
      .Concat(Nominator.DetailNegativePreferences.Select(x => new PreferenceViewModel(false, x)));

  /// <summary>
  /// The osu! preferences of the nominator.
  /// </summary>
  public IEnumerable<PreferenceViewModel> StylePreferences =>
    Nominator.OsuStylePreferences.Select(x => new PreferenceViewModel(true, $"{x} (osu!std)"))
      .Concat(Nominator.TaikoStylePreferences.Select(x => new PreferenceViewModel(false, $"{x} (osu!taiko)")))
      .Concat(Nominator.CatchStylePreferences.Select(x => new PreferenceViewModel(false, $"{x} (osu!catch)")))
      .Concat(Nominator.ManiaStylePreferences.Select(x => new PreferenceViewModel(false, $"{x} (osu!mania)")))
      .Concat(Nominator.OsuStyleNegativePreferences.Select(x => new PreferenceViewModel(false, $"{x} (osu!std)")))
      .Concat(Nominator.TaikoStyleNegativePreferences.Select(x => new PreferenceViewModel(false, $"{x} (osu!taiko)")))
      .Concat(Nominator.CatchStyleNegativePreferences.Select(x => new PreferenceViewModel(false, $"{x} (osu!catch)")))
      .Concat(Nominator.ManiaStyleNegativePreferences.Select(x => new PreferenceViewModel(false, $"{x} (osu!mania)")));

  /// <summary>
  /// Bool whether the nominator has genre preferences.
  /// </summary>
  public bool HasGenrePreferences => GenrePreferences.Any();

  /// <summary>
  /// Bool whether the nominator has language preferences.
  /// </summary>
  public bool HasLanguagePreferences => LanguagePreferences.Any();

  /// <summary>
  /// Bool whether the nominator has mapper preferences.
  /// </summary>
  public bool HasMapperPreferences => MapperPreferences.Any();

  /// <summary>
  /// Bool whether the nominator has detail preferences.
  /// </summary>
  public bool HasDetailPreferences => DetailPreferences.Any();

  /// <summary>
  /// Bool whether the nominator has style preferences.
  /// </summary>
  public bool HasStylePreferences => StylePreferences.Any();

  #endregion

  /// <summary>
  /// Bool whether the nominator has request info specified.
  /// </summary>
  public bool HasRequestInfo => Nominator.RequestInfo is not "";

  /// <summary>
  /// The request info of the nominator, formatted by trimming newlines and applying some markdown hotfixes.
  /// </summary>
  public string RequestInfoFormatted
  {
    get
    {
      return Nominator.RequestInfo.Trim('\n').Replace("\n ", "\n").Replace(":\n-", ":\n\n-").Replace("\n", "  \n");
    }
  }

  /// <summary>
  /// The last time the queue status of the nominator was updated, as a readable string.
  /// </summary>
  public string LastQueueStatusUpdate
    => IsClosed ? $"Last opened {(DateTime.UtcNow - Nominator.LastQueueStatusUpdate).Days} days ago"
                : $"Opened for {(DateTime.UtcNow - Nominator.LastQueueStatusUpdate).Days} days";

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