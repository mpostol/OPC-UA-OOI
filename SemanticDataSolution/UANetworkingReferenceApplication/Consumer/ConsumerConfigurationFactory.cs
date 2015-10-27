
using System;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  /// <summary>
  /// Class ConfigurationFactory - provides basic implementation of the <see cref="IConfigurationFactory"/>.
  /// </summary>
  /// <remarks>In production release it shall be replaced by reading a configuration file.</remarks>
  internal class ConsumerConfigurationFactory : ConfigurationFactoryBase
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerConfigurationFactory"/> class.
    /// </summary>
    public ConsumerConfigurationFactory()
    {
      Loader = ConsumerConfiguration.Load;
    }

    #region ConfigurationFactoryBase
    ///// <summary>
    ///// Occurs after the association configuration has been changed.
    ///// </summary>
    public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
    ///// <summary>
    ///// Occurs after the communication configuration has been changed.
    ///// </summary>
    public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
    #endregion

  }
}
