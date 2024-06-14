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
  private NominatorStateViewModel ViewModel { get; set; }

  /// <summary>
  /// The state displayed in the item.
  /// </summary>
  public NominatorStateViewModel State
  {
    get { return (NominatorStateViewModel)GetValue(StateProperty); }
    set
    {
      // Set the view model to the value of the state specified and update all bindings.
      // Dirty cheat around requiring a separate view model for this control.
      // The reason view models are input into the control instead of created here is that
      // the view model is shared with the more in-depth view of the nominator state,
      // so both share the same view model to update the bindings properly.
      ViewModel = value;
      Bindings.Update();

      SetValue(StateProperty, value);
    }
  }

  /// <summary>
  /// Bool whether the item displays detailed information.
  /// </summary>
  public bool IsDetailed
  {
    get { return (bool)GetValue(IsDetailedProperty); }
    set { SetValue(IsDetailedProperty, value); }
  }

  public static readonly DependencyProperty StateProperty =
      DependencyProperty.Register("State", typeof(NominatorStateViewModel), typeof(NominatorStateItem), new PropertyMetadata(null));

  public static readonly DependencyProperty IsDetailedProperty =
      DependencyProperty.Register("IsDetailed", typeof(bool), typeof(NominatorStateItem), new PropertyMetadata(null));

  public NominatorStateItem()
  {
    InitializeComponent();
  }
}
