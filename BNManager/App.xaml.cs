using Microsoft.UI.Xaml;
using BNManager.Views;
using BNManager.Services;
using BNManager.Models;

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
  protected override async void OnLaunched(LaunchActivatedEventArgs args)
  {
    await MappersGuildService.Initialize();
    ProjectService.Initialize();

    _window = new MainWindow();
    _window.Activate();
  }

  private Window _window;
}
