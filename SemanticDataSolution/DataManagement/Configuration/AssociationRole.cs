namespace UAOOI.SemanticData.DataManagement.Configuration
{
  
  /// <summary>
  /// Enum AssociationRole - enumerates UA Data Application roles. Depending on the role the <see cref="DataSetConfiguration"/> may 
  /// be associated with appropriate <see cref="MessageTransportConfiguration"/> reading or writing data. 
  /// </summary>
  public enum AssociationRole
  {
    /// <summary>
    /// The consumer role of the application
    /// </summary>
    Consumer,
    /// <summary>
    /// The producer role of the application
    /// </summary>
    Producer

  }
}
