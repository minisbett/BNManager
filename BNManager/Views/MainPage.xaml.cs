using BNManager.Models;
using BNManager.Services;
using BNManager.ViewModels;
using BNManager.Views.Controls;
using BNManager.Views.Dialogs;
using BNManager.Views.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BNManager.Views;

public sealed partial class MainPage : Page
{
  /// <summary>
  /// The title bar of the application, exposed for access from the <see cref="MainWindow"/>.
  /// </summary>
  public UIElement AppTitleBar => TitleBar;

  public MainPage()
  {
    InitializeComponent();

    // Select the home page by default.
    NavView.SelectedItem = HomeNavigationViewItem;

    Loaded += async (sender, e) =>
    {
      // Display a loading dialog while some services are being initialized.
      LoadingDialog ld = new LoadingDialog() { XamlRoot = Content.XamlRoot };
      _ = ld.ShowAsync();

      // Initialize the mappers guild service, providing info about the beatmap nominators.
      ld.InfoText = "Fetching Beatmap Nominators from Mappers' Guild...";
      await MappersGuildService.InitializeAsync();

      // Load all projects from the local storage.
      ld.InfoText = "Loading projects...";
      ProjectService.Initialize();

      // Update the nominator names saved in the projects based on the mappers guild data.
      // Also find all nominators saved in projects that are no longer part of the Beatmap Nominators, remove them and notify the user.
      List<string> removedNominators = new List<string>();
      foreach (Project project in ProjectService.Projects)
      {
        // Remove all nominator states that are no longer part of the Beatmap Nominators and remember their names for display purposes.
        removedNominators.AddRange(project.NominatorStates.Where(ns => !MappersGuildService.Nominators.Any(n => n.Id == ns.Id)).Select(ns => ns.Name));
        project.NominatorStates.RemoveAll(ns => !MappersGuildService.Nominators.Any(n => n.Id == ns.Id));

        // For all other nominator states, update the cached name based on the mappers guild data.
        foreach (NominatorState ns in project.NominatorStates)
          ns.Name = MappersGuildService.Nominators.First(n => n.Id == ns.Id).Name;
      }

      // Save all projects back to the local storage since their names might have changed via the synchronization with the mappers guild.
      ProjectService.Save();

      ld.Hide();

      // If any nominators were removed from projects, notify the user.
      if (removedNominators.Count > 0)
        _ = new ContentDialog()
        {
          XamlRoot = MainWindow.XamlRoot,
          Title = "Removed Beatmap Nominators",
          Content = "The following nominators were removed from the Beatmap Nominators and thus were removed from all projects:\n\n"
                  + $"{string.Join('\n', removedNominators.Distinct())}",
          CloseButtonText = "OK"
        }.ShowAsync();

      // Load the projects into the navigation view.
      foreach (Project p in ProjectService.Projects)
        ViewModel.ProjectNavigationItems.Add(new ProjectNavigationViewItem(p));
    };
  }

  /// <summary>
  /// Handles clicking on a navigation view item, navigating to the corresponding page.
  /// </summary>
  private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
  {
    // If no item is selected anymore (eg. because the current item got deleted), default to the home page.
    if (args.SelectedItem is null)
    {
      NavView.SelectedItem = HomeNavigationViewItem; // re-calls this event handler
      return;
    }

    // Navigate to the corresponding page, depending on the selected item.
    if (args.SelectedItem is ProjectNavigationViewItem p)
      ContentFrame.Navigate(typeof(ProjectPage), p.Project, new SuppressNavigationTransitionInfo());
    else if (args.IsSettingsSelected)
      ContentFrame.Navigate(typeof(SettingsPage));
    else if (args.SelectedItem as NavigationViewItem == HomeNavigationViewItem)
      ContentFrame.Navigate(typeof(HomePage));
    else
      throw new Exception("Invalid selected item."); // fail-safe for development

  }
}