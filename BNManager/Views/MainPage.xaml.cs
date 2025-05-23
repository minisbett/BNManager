using BNManager.Models;
using BNManager.Services;
using BNManager.Views.Controls;
using BNManager.Views.Dialogs;
using BNManager.Views.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;

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
      IAsyncOperation<ContentDialogResult> operation = ld.ShowAsync();

      // Load all projects from the local storage.
      ld.InfoText = "Loading projects...";
      ProjectService.Initialize();

      // Initialize the mappers guild service, providing info about the beatmap nominators.
      ld.InfoText = "Fetching Beatmap Nominators from Mappers' Guild...";
      try
      {
        await MappersGuildService.InitializeAsync();
        ld.Hide();
      }
      catch (BnSiteDownException)
      {
        ld.Fail("Mappers' Guild is currently unreachable.", "Proceed with cached data", MappersGuildService.IsCacheAvailable);
        await operation.AsTask().WaitAsync(CancellationToken.None); // Wait for the dialog to be actively closed (error button) before proceeding.
        MappersGuildService.InitializeFromCache();
      }

      // Update the nominator names saved in the projects based on the mappers guild data.
      // Also find all nominators that are no longer a BN, remove them and notify the user.
      // Also find all nominators that are now a BN, add them and notify the user.
      List<string> remNoms = new List<string>();
      List<string> addNoms = new List<string>();
      foreach (Project project in ProjectService.Projects)
      {
        // Find all nominator states where the nominator is no longer a BN, remove them and remember their names.
        foreach (NominatorState ns in project.NominatorStates.Where(ns => MappersGuildService.FromId(ns.Id) is null).ToArray())
        {
          remNoms.Add(ns.Name);
          project.NominatorStates.Remove(ns);
        }

        // Find all nominators that are not in the project yet, add them, and remember their names.
        foreach (Nominator nominator in MappersGuildService.Nominators.Where(n => !project.NominatorStates.Any(ns => ns.Id == n.UserId)))
        {
          project.NominatorStates.Add(new NominatorState(nominator));
          addNoms.Add($"{nominator.Name} ({string.Join(", ", nominator.ModesInfo.Select(m => m.Mode.GetName()))})");
        }
        
        // For all other nominators, update the cached name based on the mappers guild data.
        foreach (NominatorState ns in project.NominatorStates)
          ns.Name = MappersGuildService.FromId(ns.Id).Name;
      }

      // Save all projects back to the local storage since they might have changed via the synchronization with the mappers guild.
      ProjectService.Save();

      // Load the projects into the navigation view.
      foreach (Project p in ProjectService.Projects)
        ViewModel.ProjectNavigationItems.Add(new ProjectNavigationViewItem(p));

      // If any nominators were removed from projects, notify the user.
      if (remNoms.Count > 0)
        await new ContentDialog()
        {
          XamlRoot = App.MainWindow.Content.XamlRoot,
          RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light,
          Title = "Removed Beatmap Nominators",
          Content = "The following nominators were removed from the Beatmap Nominators and thus were removed from all projects:\n\n"
                  + $"{string.Join('\n', remNoms.Distinct())}",
          CloseButtonText = "OK"
        }.ShowAsync();

      // If any nominators were added to projects, notify the user.
      if (addNoms.Count > 0)
        await new ContentDialog()
        {
          XamlRoot = App.MainWindow.Content.XamlRoot,
          RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light,
          Title = "Added Beatmap Nominators",
          Content = "The following nominators added to the Beatmap Nominators and thus were added to all projects:\n\n"
                  + $"{string.Join('\n', addNoms.Distinct())}",
          CloseButtonText = "OK"
        }.ShowAsync();
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
    ContentFrame.Navigate(args switch
    {
      var _ when args.IsSettingsSelected => typeof(SettingsPage),
      var _ when args.SelectedItem as NavigationViewItem == QueueTrackerNavigationViewItem => typeof(QueueTrackerPage),
      var _ when args.SelectedItem as NavigationViewItem == HomeNavigationViewItem => typeof(HomePage),
      var _ when args.SelectedItem is ProjectNavigationViewItem => typeof(ProjectPage),
      _ => throw new NotImplementedException()
    },
    (args.SelectedItem as ProjectNavigationViewItem)?.Project,
    args.SelectedItem is ProjectNavigationViewItem ? new SuppressNavigationTransitionInfo() : null);
  }
}