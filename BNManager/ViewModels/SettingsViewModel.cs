using BNManager.Services;
using Microsoft.UI.Xaml;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the <see cref="Views.Pages.SettingsPage"/>.
/// </summary>
internal class SettingsViewModel
{
  /// <summary>
  /// Bool whether the application is in dark mode.
  /// </summary>
  public bool DarkMode
  {
    get => ConfigService.Config.DarkMode;
    set
    {
      ConfigService.Config.DarkMode = value;
      ConfigService.Save();

      (App.MainWindow.Content as FrameworkElement).RequestedTheme = value ? ElementTheme.Dark : ElementTheme.Light;
    }
  }
}
