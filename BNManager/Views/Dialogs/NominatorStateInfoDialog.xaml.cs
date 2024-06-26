using BNManager.Services;
using BNManager.ViewModels;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;

namespace BNManager.Views.Dialogs;

internal sealed partial class NominatorStateInfoDialog : ContentDialog
{
  /// <summary>
  /// The view model for the nominator state info dialog.
  /// </summary>
  private NominatorStateViewModel ViewModel { get; }

  /// <summary>
  /// Creates a new instance of <see cref="NominatorStateInfoDialog"/> with the given view model.
  /// </summary>
  /// <param name="state">The view model.</param>
  public NominatorStateInfoDialog(NominatorStateViewModel state)
  {
    InitializeComponent();
    XamlRoot = App.MainWindow.Content.XamlRoot;
    RequestedTheme = ConfigService.Config.DarkMode ? ElementTheme.Dark : ElementTheme.Light;

    ViewModel = state;
  }

  private void MarkdownTextBlock_LinkClicked(object sender, LinkClickedEventArgs e)
  {
    // Open the clicked link.
    Process.Start(new ProcessStartInfo()
    {
      FileName = e.Link,
      UseShellExecute = true
    });
  }
}
