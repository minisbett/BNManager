namespace BNManager.Enums;

/// <summary>
/// An enum containing the sort options for the nominator state list.
/// </summary>
internal enum NominatorSort
{
  /// <summary>
  /// Sorts by name in ascensing order (A-Z).
  /// </summary>
  NameAsc,

  /// <summary>
  /// Sorts by group in ascending order (Probation-NAT).
  /// </summary>
  GroupAsc,

  /// <summary>
  /// Sorts by name in descending order (Z-A).
  /// </summary>
  NameDesc,

  /// <summary>
  /// Sorts by group in descending order (NAT-Probation).
  /// </summary>
  GroupDesc
}