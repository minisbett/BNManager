using BNManager.Models;
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
  public Project Project => ViewModel.Project;

  /// <summary>
  /// Creates an instance with the specified backing project to be displayed.
  /// </summary>
  /// <param name="project">The project to display.</param>
  public ProjectNavigationViewItem(Project project)
  {
    InitializeComponent();

    ViewModel.Project = project;
  }
}
