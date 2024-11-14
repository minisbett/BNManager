using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the <see cref="Views.Pages.SettingsPage"/>.
/// </summary>
internal partial class SettingsViewModel : ObservableObject
{
  /// <summary>
  /// Bool whether the application is in dark mode.
  /// </summary>
  [ObservableProperty]
  private bool _darkMode = ConfigService.Config.DarkMode;

  partial void OnDarkModeChanged(bool value)
  {
    ConfigService.Config.DarkMode = value;
    ConfigService.Save();

    (App.MainWindow.Content as FrameworkElement).RequestedTheme = value ? ElementTheme.Dark : ElementTheme.Light;
  }

  /// <summary>
  /// The opacity of the beatmap background when a project is selected.
  /// </summary>
  [ObservableProperty]
  private int _backgroundOpacity = ConfigService.Config.BackgroundOpacity;

  partial void OnBackgroundOpacityChanged(int value)
  {
    ConfigService.Config.BackgroundOpacity = value;

    // Config is not saved automatically here as this value changes quickly while moving the slider.
  }
}
