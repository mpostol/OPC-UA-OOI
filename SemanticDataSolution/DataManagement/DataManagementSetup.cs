
using System;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Class DataManagementSetup -it is place holder to gather all external injection points used to initialize 
  /// the communication and bind to local resources.
  /// </summary>
  public class DataManagementSetup
  {

    #region Injection points
    public IBindingFactory BindingFactory { get; set; }
    public IEncodingFactory EncodingFactory { get; set; }
    public IMessageHandlerFactory MessageHandlerFactory { get; set; }
    public IConfigurationFactory ConfigurationFactory { get; set; }
    #endregion
    #region Internal control entry points
    internal AssociationsCollection AssociationsCollection { get; private set; }
    internal MessageHandlersCollection MessageHandlersCollection { get; private set; }
    #endregion

    #region Master Controll functioanlity
    public void Initialize()
    {
      if (BindingFactory == null)
        throw new ArgumentNullException();
      if (EncodingFactory == null)
        throw new ArgumentNullException();
      if (MessageHandlerFactory == null)
        throw new ArgumentNullException();
      if (ConfigurationFactory == null)
        throw new ArgumentNullException();
      ConfigurationData _configuration = ConfigurationFactory.GetConfiguration();
      AssociationsCollection = AssociationsCollection.CreateAssociations(_configuration.Associations, BindingFactory, EncodingFactory);
      ConfigurationFactory.OnAssociationConfigurationChange += AssociationsCollection.OnConfigurationChangeHandler;
      MessageHandlersCollection = MessageHandlersCollection.CreateMessageHandlers(_configuration.MessageTransport, MessageHandlerFactory, AssociationsCollection.AddMessageHandler);
      ConfigurationFactory.OnMessageHandlerConfigurationChange += MessageHandlersCollection.OnConfigurationChangeHandler;
    }
    /// <summary>
    /// Initialize and enable all associations ans start pumping the data 
    /// </summary>
    public void Run()
    {
      this.AssociationsCollection.Initialize();
      this.MessageHandlersCollection.Run();
    }
    #endregion
  }
}
