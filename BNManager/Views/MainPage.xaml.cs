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
    // If no item is selected anymore (eg. because the current item got deleted), default to the home page.
    if (args.SelectedItem is null)
    {
      NavView.SelectedItem = HomeNavigationViewItem; // re-calls this event handler
      return;
    }

    // Navigate to the corresponding page, depending on the selected item.
    if (args.SelectedItem is ProjectNavigationViewItem p)
      ContentFrame.Navigate(typeof(ProjectPage), new ProjectViewModel(p.Project), new SuppressNavigationTransitionInfo());
    else if (args.IsSettingsSelected)
      ContentFrame.Navigate(typeof(SettingsPage));
    else if (args.SelectedItem as NavigationViewItem == HomeNavigationViewItem)
      ContentFrame.Navigate(typeof(HomePage));
    else
      throw new Exception("Invalid selected item."); // fail-safe for development

  }
}