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

  /// <summary>
  /// The view model of the main page.
  /// </summary>
  private MainViewModel ViewModel { get; set; }

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

      // Initialize the view model and update the bindings.
      ViewModel = new MainViewModel(ContentFrame);
      Bindings.Update();
    };
  }
}