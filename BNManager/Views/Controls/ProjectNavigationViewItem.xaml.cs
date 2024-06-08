using BNManager.Models;
using BNManager.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BNManager.Views.Controls;

/// <summary>
/// Represents a navigation view item specifically made to display a <see cref="Project"/>.
/// </summary>
internal sealed partial class ProjectNavigationViewItem : NavigationViewItem
{
  /// <summary>
  /// The backing project this navigation view item represents.
  /// </summary>
  public Project Project { get; }

  /// <summary>
  /// The view model for this navigation view item.
  /// For obscure reasons, this cannot be created in the XAML-code directly.
  /// </summary>
  private ProjectNavigationViewItemViewModel ViewModel { get; } = new();

  /// <summary>
  /// Creates an instance with the specified backing project to be displayed.
  /// </summary>
  /// <param name="project">The project to display.</param>
  public ProjectNavigationViewItem(Project project)
  {
    Project = project;
    ViewModel.Project = project;
  }
}
