using BNManager.Enums;
using BNManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// Represents a beatmap set (project) with general info and all it's Beatmap Nominator states.
/// </summary>
internal partial class ProjectViewModel : ObservableObject
{
  /// <summary>
  /// The backing project for this view model.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private Project _project;

  /// <summary>
  /// The search query used to filter the nominators.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private string _searchQuery = "";

  /// <summary>
  /// The selected combo box item for the sort option used to sort the nominators.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private ComboBoxItem _nominatorSortItem;

  /// <summary>
  /// The selected combo box item for the preference filter option used to filter the nominators by their preferences.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private ComboBoxItem _preferenceFilterItem;

  /// <summary>
  /// The selected combo box item for the state filter option used to filter the nominators by their ask state.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private ComboBoxItem _askStateFilterItem;

  /// <summary>
  /// Bool whether only nominators with open queues should be shown.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private bool _onlyOpenQueues = false;

  /// <summary>
  /// The nominator states of the project, filtered and sorted based on the search query, sort options and filters.
  /// </summary>
  public IEnumerable<NominatorStateViewModel> NominatorStates
  {
    get
    {
      // Filter the nominator states based on the search query & modes targetted by the project.
      IEnumerable<NominatorStateViewModel> states = Project.NominatorStates.Select(state => new NominatorStateViewModel(state))
        .Where(x => SearchQuery.ToLower().Split(' ').All(tag => x.Nominator.Name.ToLower().Contains(tag)))
        .Where(state => state.Nominator.ModesInfo.Any(x => Project.Modes.Concat(new[] { Mode.None }).Contains(x.Mode)));

      // Filter the nominator states based on the state filter.
      if (AskStateFilterItem?.Tag is AskState askState)
        states = states.Where(state => state.AskState.State == askState);

      // Filter the nominator states based on whether their queue is open or not.
      if (OnlyOpenQueues)
        states = states.Where(state => !state.Nominator.RequestStatus.Contains(RequestStatus.Closed));

      // Filter the nominators based on their preferences in comparison to the project.
      if (PreferenceFilterItem?.Tag is PreferenceFilter prefFilter)
        states = states.Where(state =>
        {
          // Get the match states for the genre and language preferences for further filtering.
          // We apply a hotfix here since the Video Game genre is listed as Game in the preferences.
          string genre = (Project.Genre == "Video Game" ? "Game" : Project.Genre).ToLower();
          string language = Project.Language.ToLower();
          bool genrePref = genre == "unspecified"
                        // We concat the detail preferences to the genre preferences since BNsite splits it up like that.
                        || state.Nominator.GenrePreferences.Concat(state.Nominator.DetailPreferences).Contains(genre);
          bool languagePref = language == "unspecified" || state.Nominator.LanguagePreferences.Contains(language);
          bool anyAnti = state.Nominator.GenreNegativePreferences.Contains(genre)
                      || state.Nominator.LanguageNegativePreferences.Contains(language);

          // Filter the nominator based on the preference filter.
          return prefFilter switch
          {
            PreferenceFilter.SoftPreferred => (genrePref || languagePref) && !anyAnti,
            PreferenceFilter.ExactPreferred => genrePref && languagePref,
            PreferenceFilter.NoAntiPreferred => !anyAnti,
            _ => false
          };
        });

      // Sort the nominator states based on the selected sort option.
      return NominatorSortItem?.Tag switch
      {
        NominatorSort.NameAsc => states.OrderBy(state => state.Nominator.Name),
        NominatorSort.NameDesc => states.OrderByDescending(state => state.Nominator.Name),
        NominatorSort.GroupAsc => states.OrderBy(state => state.Nominator.ModesInfo.Max(state => state.Group))
                                        .ThenBy(state => state.Nominator.Name),
        NominatorSort.GroupDesc => states.OrderByDescending(state => state.Nominator.ModesInfo.Max(state => state.Group))
                                         .ThenBy(state => state.Nominator.Name),
        _ => states
      };
    }
  }
}