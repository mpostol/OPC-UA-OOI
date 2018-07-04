
using System;
using UAOOI.Configuration.Core;

namespace UAOOI.Configuration.DataBindings
{
  /// <summary>
  /// Class InstanceConfigurationBase - provide basic configuration of the 
  /// </summary>
  public class InstanceConfigurationBase : IInstanceConfiguration
  {
    /// <summary>
    /// Create new empty data bindings configuration for this instance node to store proprietary information of the UA server.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public void ClearConfiguration()
    {
      throw new NotImplementedException();
    }
    /// <summary>
    /// Edits this instance.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Edit()
    {
      throw new NotImplementedException();
    }
  }
}
