using BNManager.Enums;
using BNManager.Models;
using BNManager.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BNManager.ViewModels;

/// <summary>
/// The view model for the <see cref="Views.Pages.QueueTrackerPage"/>.
/// </summary>
internal partial class QueueTrackerViewModel : ObservableObject
{
  /// <summary>
  /// The selected combo box item for the mode filter option used to filter the nominators by their mode.
  /// </summary>
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(ItemsToday), nameof(ItemsYesterday), nameof(ItemsLast7Days), nameof(ItemsLast30Days), nameof(ItemsLongTimeAgo),
    nameof(HasItemsToday), nameof(HasItemsYesterday), nameof(HasItemsLast7Days), nameof(HasItemsLast30Days), nameof(HasItemsLongTimeAgo))]
  private ComboBoxItem _modeFilterItem;

  /// <summary>
  /// The nominators from the <see cref="MappersGuildService"/>, prepared with mode filtering and sort by last queue status update.
  /// </summary>
  private IEnumerable<Nominator> Nominators =>
    MappersGuildService.Nominators.Where(n => ModeFilterItem?.Tag is not Mode mode || n.ModesInfo.Any(info => info.Mode == mode))
                                  .OrderByDescending(n => n.LastQueueStatusUpdate);
     
  /// <summary>
  /// The list of items representing queue changes today (timezone-dependent).
  /// </summary>
  public IEnumerable<QueueTrackerItemViewModel> ItemsToday
    => Nominators.Where(n => n.LastQueueStatusUpdate.Date == DateTime.Today)
                 .Select(n => new QueueTrackerItemViewModel(n));

  /// <summary>
  /// The list of items representing queue changes yesterday (timezone-dependent).
  /// </summary>
  public IEnumerable<QueueTrackerItemViewModel> ItemsYesterday
    => Nominators.Where(n => n.LastQueueStatusUpdate.Date == DateTime.Today.AddDays(-1))
                 .Select(n => new QueueTrackerItemViewModel(n));

  /// <summary>
  /// The list of items representing queue changes in the last 7 days, exluding today and yesterday (timezone-dependent).
  /// </summary>
  public IEnumerable<QueueTrackerItemViewModel> ItemsLast7Days
    => Nominators.Where(n => n.LastQueueStatusUpdate >= DateTime.Today.AddDays(-6)
                          && n.LastQueueStatusUpdate < DateTime.Today.AddDays(-1))
                 .Select(n => new QueueTrackerItemViewModel(n));

  /// <summary>
  /// The list of items representing queue changes in the last 30 days, exluding the last 7 (timezone-dependent).
  /// </summary>
  public IEnumerable<QueueTrackerItemViewModel> ItemsLast30Days
    => Nominators.Where(n => n.LastQueueStatusUpdate.Date >= DateTime.Today.AddDays(-29)
                          && n.LastQueueStatusUpdate.Date < DateTime.Today.AddDays(-6))
                 .Select(n => new QueueTrackerItemViewModel(n));

  /// <summary>
  /// The list of items representing queue changes longer than 7 days ago (timezone-dependent).
  /// </summary>
  public IEnumerable<QueueTrackerItemViewModel> ItemsLongTimeAgo
    => Nominators.Where(n => n.LastQueueStatusUpdate.Date < DateTime.Today.AddDays(-29))
                 .Select(n => new QueueTrackerItemViewModel(n));

  /// <summary>
  /// Bool whether there are any items for today.
  /// </summary>
  public bool HasItemsToday => ItemsToday.Any();

  /// <summary>
  /// Bool whether there are any items for yesterday.
  /// </summary>
  public bool HasItemsYesterday => ItemsYesterday.Any();

  /// <summary>
  /// Bool whether there are any items for the last 7 days.
  /// </summary>
  public bool HasItemsLast7Days => ItemsLast7Days.Any();

  /// <summary>
  /// Bool whether there are any items for the last 30 days.
  /// </summary>
  public bool HasItemsLast30Days => ItemsLast30Days.Any();

  /// <summary>
  /// Bool whether there are any items longer than 30 days ago.
  /// </summary>
  public bool HasItemsLongTimeAgo => ItemsLongTimeAgo.Any();
}
