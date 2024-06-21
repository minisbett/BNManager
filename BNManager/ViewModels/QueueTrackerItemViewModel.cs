using BNManager.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System.Linq;
using BNManager.Enums;
using System;

namespace BNManager.ViewModels;

/// <summary>
/// Represents a queue tracker item in the <see cref="QueueTrackerItemViewModel"/> and <see cref="Views.Pages.QueueTracker"/> page.
/// </summary>
internal class QueueTrackerItemViewModel
{
  /// <summary>
  /// The nominator of the queue tracker item.
  /// </summary>
  public Nominator Nominator;

  /// <summary>
  /// A URL to the avatar of the nominator.
  /// </summary>
  public string AvatarUrl => $"https://a.ppy.sh/{Nominator.Id}";

  /// <summary>
  /// The foreground color for the queue status indicator.
  /// </summary>
  public SolidColorBrush QueueStatusForeground
    => new SolidColorBrush(Nominator.RequestStatus.Contains(RequestStatus.Closed) ? Colors.IndianRed : Colors.LightGreen);

  /// <summary>
  /// A string representation of the nominator's last queue status update.
  /// </summary>
  public string LastQueueStatusUpdate
  {
    get
    {
      int days = (DateTime.Today - Nominator.LastQueueStatusUpdate.Date).Days;
      string time = Nominator.LastQueueStatusUpdate.ToString("t");

      return days == 0 ? $"Today at {time}" : days == 1 ? $"Yesterday at {time}" : $"{days} days ago";
    }
  }

  /// <summary>
  /// Creates a new instance of the <see cref="QueueTrackerItemViewModel"/> class with the specified nominator.
  /// </summary>
  /// <param name="nominator">The nominator represented.</param>
  public QueueTrackerItemViewModel(Nominator nominator)
  {
    Nominator = nominator;
  }
}
