using BNManager.Models;
using BNManager.Services;
using BNManager.ViewModels;
using BNManager.Views.Controls;
using BNManager.Views.Dialogs;
using BNManager.Views.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

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
    NavView.SelectedItem = NavView.FooterMenuItems[0];

    Loaded += async (sender, e) =>
    {
      // Display a loading dialog while some services are being initialized.
      LoadingDialog ld = new LoadingDialog() { XamlRoot = Content.XamlRoot };
      _ = ld.ShowAsync();

      // Initialize the services and hide the loading dialog afterwards.
      ld.InfoText = "Fetching Beatmap Nominators from Mappers' Guild...";
      await MappersGuildService.InitializeAsync();
      ld.InfoText = "Loading projects...";
      ProjectService.Initialize();
      ld.Hide();

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
    // Navigate to the corresponding page, depending on the selected item.
    if (args.SelectedItem is ProjectNavigationViewItem p)
      ContentFrame.Navigate(typeof(ProjectPage), new ProjectViewModel(p.Project), new SuppressNavigationTransitionInfo());
    else if (args.IsSettingsSelected)
      ContentFrame.Navigate(typeof(SettingsPage));
    else
      ContentFrame.Navigate(typeof(HomePage));
  }
}