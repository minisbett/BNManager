using BNManager.Models;
using BNManager.Services;
using BNManager.ViewModels;
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

      // Load the projects into the view model.
      foreach (Project p in ProjectService.Projects)
        ViewModel.Projects.Add(new ProjectViewModel(p));
    };
  }

  /// <summary>
  /// Handles clicking on a navigation view item, navigating to the corresponding page.
  /// </summary>
  private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
  {
    // This happens when the item source gets updated (eg. when a project is deleted). In this case, we default to the home page.
    if (args.SelectedItem is null)
    {
      NavView.SelectedItem = NavView.FooterMenuItems[0];
      return;
    }

    // Navigate to the corresponding page, depending on the selected item.
    if (args.SelectedItem is ProjectViewModel p)
      ContentFrame.Navigate(typeof(ProjectPage), p, new SuppressNavigationTransitionInfo());
    else if (args.IsSettingsSelected)
      ContentFrame.Navigate(typeof(SettingsPage));
    else
      ContentFrame.Navigate(typeof(HomePage), null);
  }
}

/// <summary>
/// The data template selector for the navigation view, separating between the project and normal items.
/// </summary>
internal class NavigationViewDataTemplateSelector : DataTemplateSelector
{
  /// <summary>
  /// The project template for <see cref="ProjectViewModel"/>s.
  /// </summary>
  public DataTemplate ProjectTemplate { get; set; }

  /// <summary>
  /// The default template for <see cref="NavigationViewItem"/>s.
  /// </summary>
  public DataTemplate DefaultTemplate { get; set; }

  protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
  {
    return item switch
    {
      ProjectViewModel => ProjectTemplate,
      _ => DefaultTemplate
    };
  }
}