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
  /// The selected combo box item for the state filter option used to filter the nominators by their ask state.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(NominatorStates))]
  private ComboBoxItem _askStateFilterItem;

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
      if (AskStateFilterItem?.Tag is AskState state)
        states = states.Where(states => states.AskState.State == state);

      // Filter the nominator states based on the modes targetted by the project.
      if (!IgnoreModes)
        states = states.Where(state => state.Nominator.ModesInfo.Any(x => Project.Modes.Contains(x.Mode)));

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