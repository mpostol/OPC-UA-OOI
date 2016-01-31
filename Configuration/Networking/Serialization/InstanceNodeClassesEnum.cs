
using System.Runtime.Serialization;

namespace UAOOI.Configuration.Networking.Serialization
{
  /// <summary>
  /// Enumeration of the node classes that can be a source of process data.
  /// </summary>
  [DataContractAttribute(Name = "InstanceNodeClassesEnum", Namespace = CommonDefinitions.Namespace)]
  public enum InstanceNodeClassesEnum
  {
    /// <summary>
    /// Object NodeClasses
    /// </summary>
    [EnumMemberAttribute()]
    Object,
    /// <summary>
    /// Variable NodeClasses
    /// </summary>
    [EnumMemberAttribute()]
    Variable,
    /// <summary>
    /// Method NodeClasses
    /// </summary>
    [EnumMemberAttribute()]
    Method,
    /// <summary>
    /// View NodeClasses
    /// </summary>
    [EnumMemberAttribute()]
    View,
    /// <summary>
    /// Not defined or nor relevant
    /// </summary>
    [EnumMemberAttribute()]
    NotDefined

  }
}