using Microsoft.UI.Xaml.Controls;

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
}
