using BNManager.Models;
using BNManager.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views.Controls;

/// <summary>
/// Represents a nominator state item in the project view.
/// </summary>
internal sealed partial class NominatorStateItem : UserControl
{
  /// <summary>
  /// The view model of the nominator state item.
  /// </summary>
  private NominatorStateViewModel ViewModel { get; } = new NominatorStateViewModel();

  public NominatorState State
  {
    get { return (NominatorState)GetValue(StateProperty); }
    set { SetValue(StateProperty, value); }
  }

  public static readonly DependencyProperty StateProperty =
      DependencyProperty.Register("State", typeof(NominatorState), typeof(NominatorStateItem), new PropertyMetadata(null));

  public NominatorStateItem()
  {
    InitializeComponent();

    Loaded += (sender, e) =>
    {
      // Load the state into the view model after the control has been loaded.
      ViewModel.State = State;
    };
  }
}
