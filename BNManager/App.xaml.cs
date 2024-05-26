using BNManager.Views;
using Microsoft.UI.Xaml;
using System.Globalization;

namespace BNManager;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
  /// <summary>
  /// The XamlRoot for the main window of the application.
  /// </summary>
  public static XamlRoot XamlRoot { get; private set; }

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

    // Create the main window, activate it and set the XamlRoot for the entire application.
    _window = new MainWindow();
    _window.Activate();
    XamlRoot = _window.Content.XamlRoot;
  }

  private Window _window;
}
