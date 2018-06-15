
using System;
using System.ComponentModel.Composition;
using UAOOI.Configuration.Networking;
using UAOOI.Networking.ReferenceApplication.Diagnostic;
using UAOOI.Networking.SemanticData;

namespace UAOOI.Networking.ReferenceApplication.Producer
{

  /// <summary>
  /// Class OPCUAServerProducerSimulator simulates interface to internal <see cref="CustomNodeManager"/> class.
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
    [Import(typeof(IProducerViewModel))]
    internal IProducerViewModel ViewModel
    {
      get; set;
    }
    /// <summary>
    /// Sets the producer configuration factory.
    /// </summary>
    /// <value>The producer configuration factory.</value>
    [Import(ProducerCompositionSettings.ConfigurationFactoryContract, typeof(IConfigurationFactory))]
    public IConfigurationFactory ProducerConfigurationFactory
    {
      set { ConfigurationFactory = value; }
    }
    /// <summary>
    /// Sets the producer encoding factory.
    /// </summary>
    /// <value>The producer encoding factory.</value>
    [Import(ProducerCompositionSettings.EncodingFactoryContract, typeof(IEncodingFactory))]
    public IEncodingFactory ProducerEncodingFactory
    {
      set { EncodingFactory = value; }
    }
    /// <summary>
    /// Sets the producer binding factory.
    /// </summary>
    /// <value>The producer binding factory.</value>
    [Import(ProducerCompositionSettings.BindingFactoryContract, typeof(IBindingFactory))]
    public IBindingFactory ProducerBindingFactory
    {
      set { BindingFactory = value; }
    }
    #endregion

    #region API
    internal void Setup()
    {
      try
      {
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(OPCUAServerProducerSimulator)}.{nameof(Setup)} starting");
        ViewModel.ProducerRestart += (sender, e) => { };
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
