using BNManager.Enums;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using System;
using System.Linq;
using Windows.UI;

namespace BNManager.ViewModels;

/// <summary>
/// Represents a displayable ask state with it's display name and enum value.
/// </summary>
internal class AskStateViewModel
{
    /// <summary>
    /// The ask state.
    /// </summary>
    public AskState State { get; }

    /// <summary>
    /// The color of the displayed ask state.
    /// </summary>
    public Brush Brush => new SolidColorBrush(State switch
    {
        AskState.Declined => Color.FromArgb(255, 224, 102, 102),
        AskState.NotAsked => Color.FromArgb(255, 200, 200, 200),
        AskState.Pending => Color.FromArgb(255, 255, 217, 102),
        AskState.Possible => Color.FromArgb(255, 153, 204, 255),
        AskState.Confirmed => Color.FromArgb(255, 76, 212, 112),
        _ => throw new ArgumentOutOfRangeException(nameof(State), State, null)
    });

    public AskStateViewModel(AskState state)
    {
        State = state;
    }

    public override string ToString() => State switch
    {
        AskState.Declined => "Declined",
        AskState.NotAsked => "Not Asked",
        AskState.Pending => "Pending",
        AskState.Possible => "Possible",
        AskState.Confirmed => "Confirmed",
        _ => throw new ArgumentOutOfRangeException(nameof(State), State, null)
    };

    /// <summary>
    /// An array of options for the nominator ask state combo box.
    /// </summary>
    public static AskStateViewModel[] Options { get; } = Enum.GetValues<AskState>().Select(x => new AskStateViewModel(x)).ToArray();
}