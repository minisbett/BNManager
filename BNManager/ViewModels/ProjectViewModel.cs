using BNManager.Enums;
using BNManager.Models;
using BNManager.Services;
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
  public IEnumerable<NominatorState> NominatorStates
  {
    get
    {
      // Filter the nominator states based on the search query.
      IEnumerable<NominatorState> states = Project.NominatorStates
        .Where(x => SearchQuery.ToLower().Split(' ').All(tag => x.Name.ToLower().Contains(tag)));

      // Filter the nominator states based on the state filter.
      if (AskStateFilterItem?.Tag is AskState state)
        states = states.Where(states => states.AskState == state);

      // Filter the nominator states based on the modes targetted by the project.
      Nominator nom(NominatorState state) => MappersGuildService.Nominators.FirstOrDefault(x => x.Name == state.Name);
      if (!IgnoreModes)
        states = states.Where(state => nom(state).ModesInfo.Any(x => Project.Modes.Contains(x.Mode)));

      // Sort the nominator states based on the selected sort option.
      return NominatorSortItem?.Tag switch
      {
        NominatorSort.NameAsc => states.OrderBy(state => state.Name),
        NominatorSort.NameDesc => states.OrderByDescending(state => state.Name),
        NominatorSort.GroupAsc => states.OrderBy(state => nom(state).ModesInfo.Max(x => x.Group)).ThenBy(x => x.Name),
        NominatorSort.GroupDesc => states.OrderByDescending(state => nom(state).ModesInfo.Max(x => x.Group)).ThenBy(x => x.Name),
        _ => states
      };
    }
  }
}