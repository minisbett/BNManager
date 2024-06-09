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
  private ComboBoxItem _sort;

  /// <summary>
  /// The selected combo box item for the state filter option used to filter the nominators by their ask state.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private ComboBoxItem _askStateFilter;

  /// <summary>
  /// Bool whether the mode targetted by the project are being ignored.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private bool _ignoreModes = false;

  /// <summary>
  /// The nominator states of the project, filtered and sorted based on the search query, sort options and filters.
  /// </summary>
  public IEnumerable<NominatorStateViewModel> NominatorStates
  {
    get
    {
      // Filter the nominator states based on the search query.
      IEnumerable<NominatorStateViewModel> states = Project.NominatorStates.Select(state => new NominatorStateViewModel(state))
        .Where(x => SearchQuery.ToLower().Split(' ').All(tag => x.Nominator.Name.ToLower().Contains(tag)));

      // Filter the nominator states based on the state filter.
      if (AskStateFilter?.Tag is AskState state)
        states = states.Where(states => states.State.State == state);

      // Filter the nominator states based on the modes targetted by the project.
      if (!IgnoreModes)
        states = states.Where(x => x.Nominator.ModesInfo.Any(x => Project.Modes.Contains(x.Mode)));

      // Sort the nominator states based on the selected sort option.
      return Sort?.Tag switch
      {
        SortOption.NameAsc => states.OrderBy(x => x.Nominator.Name),
        SortOption.NameDesc => states.OrderByDescending(x => x.Nominator.Name),
        SortOption.LevelAsc => states.OrderBy(x => x.Nominator.ModesInfo.Max(x => x.Group)).ThenBy(x => x.Nominator.Name),
        SortOption.LevelDesc => states.OrderByDescending(x => x.Nominator.ModesInfo.Max(x => x.Group)).ThenBy(x => x.Nominator.Name),
        _ => states
      };
    }
  }
}