using BNManager.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;

namespace BNManager.Views;

public sealed partial class HomePage : Page
{
  private HomeViewModel ViewModel { get; }

  /// <summary>
  /// Creates a new instance of the <see cref="HomePage"/> with the specified command for creating a new project.
  /// </summary>
  /// <param name="createProjectCommand"></param>
  public HomePage(IAsyncRelayCommand createProjectCommand)
  {
    ViewModel = new HomeViewModel(createProjectCommand);

    InitializeComponent();
  }
}
