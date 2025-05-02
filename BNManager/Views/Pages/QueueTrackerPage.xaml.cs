using BNManager.Models;
using BNManager.ViewModels;
using BNManager.Views.Dialogs;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.System;

namespace BNManager.Views.Pages;

/// <summary>
/// The page representing the queue tracker, showing the latest queue updates of all nominators.
/// </summary>
public sealed partial class QueueTrackerPage : Page
{
  public QueueTrackerPage()
  {
    InitializeComponent();

    // Set the default selection for the mode filter combo box.
    ViewModel.ModeFilterItem = ModeFilterDefaultItem;
  }

  private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
  {
    Nominator nominator = (sender as Button).Tag as Nominator;
    await Launcher.LaunchUriAsync(new Uri($"https://bn.mappersguild.com/home?id={nominator.Id}"));
  }
}
