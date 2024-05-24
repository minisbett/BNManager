using BNManager.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace BNManager.Views;

public sealed partial class MainPage : Page
{
  public static new XamlRoot XamlRoot { get; private set; }

  public UIElement AppTitleBar => TitleBar;

  public MainPage()
  {
    InitializeComponent();

    Loaded += async (sender, e) =>
    {
      // Make the xaml root available so that it can show dialogs.
      XamlRoot = Content.XamlRoot;

      // Display a loading dialog while some services are being initialized.
      LoadingDialog ld = new LoadingDialog() { XamlRoot = Content.XamlRoot };
      _ = ld.ShowAsync();

      // Initialize the services and hide the loading dialog afterwards.
      ld.InfoText = "Fetching Beatmap Nominators from Mappers' Guild...";
      await MappersGuildService.InitializeAsync();
      ld.InfoText = "Loading projects...";
      ProjectService.Initialize();
     ld.Hide();
    };
  }
}
