
using System;
using System.ComponentModel.Composition;
using UAOOI.Configuration.Networking;
using UAOOI.Networking.ReferenceApplication.Diagnostic;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.MessageHandling;
using UAOOI.Networking.SimulatorInteroperabilityTest;

namespace UAOOI.Networking.ReferenceApplication.Producer
{

  /// <summary>
  /// Class OPCUAServerProducerSimulator represents a data producer in the Reference Application. It is responsible to compose all parts making up a producer.
  /// </summary>
  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  internal sealed class OPCUAServerProducerSimulator : DataManagementSetup
  {

    #region Composition
    /// <summary>
    /// Gets or sets the view model to be used for diagnostic purpose..
    /// </summary>
    /// <value>The view model.</value>
    [Import(typeof(SimulatorViewModel))]
    internal SimulatorViewModel ViewModel
    {
      get; set;
    }
    /// <summary>
    /// Sets the producer configuration factory.
    /// </summary>
    /// <value>The producer configuration factory.</value>
    [Import(SimulatorCompositionSettings.ConfigurationFactoryContract, typeof(IConfigurationFactory))]
    public IConfigurationFactory ProducerConfigurationFactory
    {
      set { ConfigurationFactory = value; }
    }
    /// <summary>
    /// Sets the producer encoding factory.
    /// </summary>
    /// <value>The producer encoding factory.</value>
    [Import(typeof(IEncodingFactory))]
    public IEncodingFactory ProducerEncodingFactory
    {
      set { EncodingFactory = value; }
    }
    /// <summary>
    /// Sets the producer binding factory.
    /// </summary>
    /// <value>The producer binding factory.</value>
    [Import(SimulatorCompositionSettings.BindingFactoryContract, typeof(IBindingFactory))]
    public IBindingFactory ProducerBindingFactory
    {
      set { BindingFactory = value; }
    }
    [Import(typeof(IMessageHandlerFactory))]
    public IMessageHandlerFactory ProducerMessageHandlerFactory
    {
      set { MessageHandlerFactory = value; }
    }
    #endregion

    #region API
    internal void Setup()
    {
      try
      {
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(OPCUAServerProducerSimulator)}.{nameof(Setup)} starting");
        ViewModel.ChangeProducerCommand(() => { ViewModel.ProducerErrorMessage = "Restarted"; });
        Start();
        ViewModel.ProducerErrorMessage = "Running";
        ReferenceApplicationEventSource.Log.Initialization($" Setup of the producer engine acomplished and starting sending data.");
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.LogException(_ex);
        ViewModel.ProducerErrorMessage = "ERROR";
        Dispose();
      }
    }
    #endregion

  }

}
