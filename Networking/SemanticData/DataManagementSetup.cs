
using System;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Configuration.Networking;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData
{
  /// <summary>
  /// Class DataManagementSetup - it is place holder to gather all external injection points used to initialize 
  /// the communication and bind to local resources.
  /// </summary>
  public class DataManagementSetup : IDisposable
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

    #region private
    /// <summary>
    /// Starts this instance - Initializes the data set infrastructure, enable all associations ans start pumping the data;
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
    protected void Start()
    {
      Initialize();
      Run();
    }
    /// <summary>
    /// Initializes the data set infrastructure.
    /// </summary>
    private void Initialize()
    {
      if (BindingFactory == null)
        throw new ArgumentNullException(nameof(BindingFactory));
      if (EncodingFactory == null)
        throw new ArgumentNullException(nameof(EncodingFactory));
      if (MessageHandlerFactory == null)
        throw new ArgumentNullException(nameof(MessageHandlerFactory));
      if (ConfigurationFactory == null)
        throw new ArgumentNullException(nameof(ConfigurationFactory));
      DisposeMessageHandlersCollection();
      ConfigurationData _configuration = ConfigurationFactory.GetConfiguration();
      AssociationsCollection = AssociationsCollection.CreateAssociations(_configuration.DataSets, BindingFactory, EncodingFactory);
      ConfigurationFactory.OnAssociationConfigurationChange += AssociationsCollection.OnConfigurationChangeHandler;
      MessageHandlersCollection = MessageHandlersCollection.CreateMessageHandlers(_configuration.MessageHandlers, MessageHandlerFactory, EncodingFactory, AssociationsCollection.AddMessageHandler);
      ConfigurationFactory.OnMessageHandlerConfigurationChange += MessageHandlersCollection.OnConfigurationChangeHandler;
    }
    /// <summary>
    /// Initialize and enable all associations ans start pumping the data 
    /// </summary>
    private void Run()
    {
      if (AssociationsCollection == null)
        throw new ArgumentNullException(nameof(AssociationsCollection));
      if (MessageHandlersCollection == null)
        throw new ArgumentNullException(nameof(MessageHandlersCollection));
      this.AssociationsCollection.Initialize();
      this.MessageHandlersCollection.Run();
    }
    #endregion

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    protected virtual void Dispose(bool disposing)
    {
      if (disposedValue)
        return;
      if (disposing)
      {
        DisposeMessageHandlersCollection();
      }
      // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
      // TODO: set large fields to null.
      disposedValue = true;
    }
    private void DisposeMessageHandlersCollection()
    {
      if (MessageHandlersCollection == null)
        return;
      foreach (IMessageHandler _handler in MessageHandlersCollection.Values)
        _handler.Dispose();
      MessageHandlersCollection = null; //to make sure no one will use them anymore.
    }
    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion

  }
}
