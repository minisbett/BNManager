using BNManager.Enums;
using BNManager.Models;
using BNManager.Services;
using BNManager.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BNManager.ViewModels;

/// <summary>
/// Represents a beatmap set (project) with general info and all it's Beatmap Nominator states.
/// </summary>
internal partial class ProjectViewModel : ObservableObject
{
  /// <summary>
  /// The nominator state view models of this project.
  /// </summary>
  private readonly NominatorStateViewModel[] _nominatorStates;

  /// <summary>
  /// The delete project command, called when the project is being deleted.
  /// </summary>
  private readonly IRelayCommand<ProjectViewModel> _deleteProjectCommand;

  /// <summary>
  /// The backing project for this view model.
  /// </summary>
  private readonly Project _project;

  /// <summary>
  /// The name of the project.
  /// </summary>
  public string Name
  {
    get => _project.Name;
    set
    {
      _project.Name = value;
      OnPropertyChanged(nameof(Name));
    }
  }

  /// <summary>
  /// The modes targetted by the project.
  /// </summary>
  public Mode[] Modes
  {
    get => _project.Modes;
    set
    {
      _project.Modes = value;
      OnPropertyChanged(nameof(Modes));
    }
  }

  /// <summary>
  /// A URL to the cover image of the project.
  /// </summary>
  public string Cover => $"https://assets.ppy.sh/beatmaps/{_project.BeatmapSetId}/covers/cover.jpg";

  /// <summary>
  /// The search query used to filter the nominators.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FilteredNominatorStates))]
  private string _searchQuery = "";

  /// <summary>
  /// The sort option used to sort the nominators.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FilteredNominatorStates))]
  private SortOptionViewModel _sort = SortOptionViewModel.Options[0];

  /// <summary>
  /// The state filter option used to filter the nominators by their ask state.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FilteredNominatorStates))]
  private DisplayableStateFilterOption _stateFilter = DisplayableStateFilterOption.Options[0];

  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FilteredNominatorStates))]
  private bool _ignoreModes = false;

  /// <summary>
  /// The <see cref="_nominatorStates"/>, filtered and sorted based on the search query, sort options and filters.
  /// </summary>
  public IEnumerable<NominatorStateViewModel> FilteredNominatorStates
  {
    get
    {
      // Filter the nominator states based on the search query.
      IEnumerable<NominatorStateViewModel> states = _nominatorStates
        .Where(x => SearchQuery.ToLower().Split(' ').All(tag => x.Nominator.Name.ToLower().Contains(tag)));

      // Filter the nominator states based on the state filter.
      if (StateFilter.State != null)
        states = states.Where(states => states.State.State == StateFilter.State);

      // Filter the nominator states based on the modes targetted by the project.
      if (!IgnoreModes)
        states = states.Where(x => x.Nominator.ModesInfo.Any(x => _project.Modes.Contains(x.Mode)));

      // Sort the nominator states based on the selected sort option.
      return Sort.Option switch
      {
        SortOption.NameAsc => states.OrderBy(x => x.Nominator.Name),
        SortOption.NameDesc => states.OrderByDescending(x => x.Nominator.Name),
        SortOption.LevelAsc => states.OrderBy(x => x.Nominator.ModesInfo.Max(x => x.Group)),
        SortOption.LevelDesc => states.OrderByDescending(x => x.Nominator.ModesInfo.Max(x => x.Group)),
        _ => states
      };
    }
  }

  public ProjectViewModel(Project project, IRelayCommand<ProjectViewModel> deleteProjectCommand)
  {
    _project = project;
    _deleteProjectCommand = deleteProjectCommand;

    // Load all nominator states as their view models.
    _nominatorStates = project.NominatorStates.Select(x => new NominatorStateViewModel(x)).ToArray();
  }

  /// <summary>
  /// Opens the beatmap set in the web browser.
  /// </summary>
  [RelayCommand]
  private void OpenBeatmapSetInWeb()
  {
    Process.Start(new ProcessStartInfo()
    {
      FileName = $"https://osu.ppy.sh/s/{_project.BeatmapSetId}",
      UseShellExecute = true
    });
  }

  /// <summary>
  /// Prompts the user to edit a project.
  /// </summary>
  [RelayCommand]
  private async Task EditProject()
  {
    EditProjectDialog pd = new EditProjectDialog(_project)
    {
      XamlRoot = MainPage.XamlRoot
    };

    if (await pd.ShowAsync() == ContentDialogResult.Primary)
    {
      EditProjectDialogViewModel pdvm = pd.DataContext as EditProjectDialogViewModel;

      // Get a list of all targetted modes.
      List<Mode> modes = new List<Mode>();
      if (pdvm.StdEnabled) modes.Add(Mode.Standard);
      if (pdvm.TaikoEnabled) modes.Add(Mode.Taiko);
      if (pdvm.CtbEnabled) modes.Add(Mode.Catch);
      if (pdvm.ManiaEnabled) modes.Add(Mode.Mania);

      // Update the project and UI and save.
      Name = pdvm.Name;
      Modes = modes.ToArray();
      ProjectService.Save();
    }
  }

  /// <summary>
  /// Prompts the user to confirm the deletion of the project by running the delete project command from the <see cref="MainPage"/>.
  /// </summary>
  [RelayCommand]
  private void DeleteProject() => _deleteProjectCommand.Execute(this);
}