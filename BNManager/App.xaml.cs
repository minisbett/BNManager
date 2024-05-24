using BNManager.Views;
using Microsoft.UI.Xaml;
using System.Globalization;

namespace BNManager;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
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

    _window = new MainWindow();
    _window.Activate();
  }

  private Window _window;
}
