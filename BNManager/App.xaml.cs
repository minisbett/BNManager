using BNManager.Services;
using BNManager.Views;
using Microsoft.UI.Xaml;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace BNManager;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
  /// <summary>
  /// The version of the application.
  /// </summary>
  public static string Version
  {
    get
    {
      string ver = "0.5.0";
#if DEBUG
      ver += "-dev";
#endif

      return ver;
    }
  }

  /// <summary>
  /// The main window of the application.
  /// </summary>
  public static Window MainWindow { get; } = new MainWindow();

  public App()
  {
    InitializeComponent();
  }

  /// <summary>
  /// Invoked when the application is launched.
  /// </summary>
  /// <param name="args">Details about the launch request and process.</param>
  protected override void OnLaunched(LaunchActivatedEventArgs args)
  {
    // Ensure an invariant culture for the entire application for parsing and formatting reasons.
    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

    // Initialize the configuration service by loading the config file.
    ConfigService.Load();

    // Apply settings from the configuration which are to be applied on startup.
    (MainWindow.Content as FrameworkElement).RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light;

    MainWindow.Activate();
  }
}
