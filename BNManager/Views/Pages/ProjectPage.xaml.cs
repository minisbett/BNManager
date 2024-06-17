using BNManager.Enums;
using BNManager.Models;
using BNManager.ViewModels;
using BNManager.Views.Dialogs;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BNManager.Views.Pages;

/// <summary>
/// The page representing a singular <see cref="Project"/>.
/// </summary>
internal sealed partial class ProjectPage : Page
{
  public ProjectPage()
  {
    InitializeComponent();

    // Set the default selection for the combo boxes. Note that we are specifically setting the tag here because at this
    // point the bindings, which assign enum value and combobox item, haven't initialized yet, causing Tag to be null.
    NominatorSortDefaultItem.Tag = NominatorSort.NameAsc;
    ViewModel.NominatorSortItem = NominatorSortDefaultItem;
    ViewModel.AskStateFilterItem = AskStateFilterDefaultItem;
    ViewModel.PreferenceFilterItem = PreferenceFilterDefaultItem;
  }

  protected override void OnNavigatedTo(NavigationEventArgs e)
  {
    base.OnNavigatedTo(e);

    // Set the project in the view model to the navigation parameter.
    ViewModel.Project = e.Parameter as Project;
  }

  private void ListView_ItemClick(object sender, ItemClickEventArgs e)
  {
    _ = new NominatorStateInfoDialog(e.ClickedItem as NominatorStateViewModel).ShowAsync();
  }
}
