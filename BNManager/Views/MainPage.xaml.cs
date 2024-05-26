using BNManager.Services;
using BNManager.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

    Loaded += async (sender, e) =>
    {
      //NavigationView.SelectedItem = NavigationView.FooterMenuItems[0];

      // Display a loading dialog while some services are being initialized.
      LoadingDialog ld = new LoadingDialog() { XamlRoot = Content.XamlRoot };
      _ = ld.ShowAsync();

      // Initialize the services and hide the loading dialog afterwards.
      ld.InfoText = "Fetching Beatmap Nominators from Mappers' Guild...";
      await MappersGuildService.InitializeAsync();
      ld.InfoText = "Loading projects...";
      ProjectService.Initialize();
      ld.Hide();

      // Update the bindings as the projects have been loaded now.
      Bindings.Update();
    };
  }

  private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
  {
    if (args.SelectedItem is ProjectViewModel p)
      ContentFrame.Navigate(typeof(ProjectPage), p);
    else if (args.IsSettingsSelected)
      ContentFrame.Navigate(typeof(SettingsPage));
    else
      ContentFrame.Navigate(typeof(HomePage));
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