
using System;
using UAOOI.SemanticData.DataManagement.MessageHandling;
using UAOOI.SemanticData.UANetworking.Configuration;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Class DataManagementSetup - it is place holder to gather all external injection points used to initialize 
  /// the communication and bind to local resources.
  /// </summary>
  public class DataManagementSetup
  {

    #region Injection points
    /// <summary>
    /// Gets or sets the binding factory.
    /// </summary>
    /// <value>The binding factory.</value>
    public IBindingFactory BindingFactory { get; set; }
    /// <summary>
    /// Gets or sets the encoding factory.
    /// </summary>
    /// <value>The encoding factory.</value>
    public IEncodingFactory EncodingFactory { get; set; }
    /// <summary>
    /// Gets or sets the message handler factory.
    /// </summary>
    /// <value>The message handler factory.</value>
    public IMessageHandlerFactory MessageHandlerFactory { get; set; }
    /// <summary>
    /// Gets or sets the configuration factory.
    /// </summary>
    /// <value>The configuration factory.</value>
    public IConfigurationFactory ConfigurationFactory { get; set; }
    #endregion

    #region Internal control entry points
    /// <summary>
    /// Gets the associations collection.
    /// </summary>
    /// <value>The associations collection.</value>
    internal AssociationsCollection AssociationsCollection { get; private set; }
    /// <summary>
    /// Gets the message handlers collection.
    /// </summary>
    /// <value>The message handlers collection.</value>
    internal MessageHandlersCollection MessageHandlersCollection { get; private set; }
    #endregion

    #region Master Controll functioanlity
    /// <summary>
    /// Initializes the data set infrastructure.
    /// </summary>
    /// <exception cref="System.ArgumentNullException">
    /// BindingFactory
    /// or
    /// EncodingFactory
    /// or
    /// MessageHandlerFactory
    /// or
    /// ConfigurationFactory
    /// </exception>
    public void Initialize()
    {
      if (BindingFactory == null)
        throw new ArgumentNullException(nameof(BindingFactory));
      if (EncodingFactory == null)
        throw new ArgumentNullException(nameof(EncodingFactory));
      if (MessageHandlerFactory == null)
        throw new ArgumentNullException(nameof(MessageHandlerFactory));
      if (ConfigurationFactory == null)
        throw new ArgumentNullException(nameof(ConfigurationFactory));
      ConfigurationData _configuration = ConfigurationFactory.GetConfiguration();
      AssociationsCollection = AssociationsCollection.CreateAssociations(_configuration.DataSets, BindingFactory, EncodingFactory);
      ConfigurationFactory.OnAssociationConfigurationChange += AssociationsCollection.OnConfigurationChangeHandler;
      MessageHandlersCollection = MessageHandlersCollection.CreateMessageHandlers(_configuration.MessageHandlers, MessageHandlerFactory, EncodingFactory, AssociationsCollection.AddMessageHandler);
      ConfigurationFactory.OnMessageHandlerConfigurationChange += MessageHandlersCollection.OnConfigurationChangeHandler;
    }
    /// <summary>
    /// Initialize and enable all associations ans start pumping the data 
    /// </summary>
    public void Run()
    {
      if (AssociationsCollection == null)
        throw new ArgumentNullException(nameof(AssociationsCollection));
      if (MessageHandlersCollection == null)
        throw new ArgumentNullException(nameof(MessageHandlersCollection));
      this.AssociationsCollection.Initialize();
      this.MessageHandlersCollection.Run();
    }
    #endregion
  }
}
