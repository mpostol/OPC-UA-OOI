
using CommonServiceLocator;
using System;
using System.ComponentModel.Composition;
using UAOOI.Networking.ReferenceApplication.Core.Diagnostic;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.SemanticData.MessageHandling;

namespace UAOOI.Networking.SimulatorInteroperabilityTest
{

  /// <summary>
  /// Class SimulatorDataManagementSetup represents a data producer in the Reference Application. It is responsible to compose all parts making up a producer.
  /// </summary>
  [Export]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public sealed class SimulatorDataManagementSetup : DataManagementSetup
  {
    #region Composition
    /// <summary>
    /// Initializes a new instance of the <see cref="SimulatorDataManagementSetup"/> class.
    /// </summary>
    public SimulatorDataManagementSetup()
    {
      IServiceLocator _serviceLocator = ServiceLocator.Current;
      string _configurationFileName = _serviceLocator.GetInstance<string>(SimulatorCompositionSettings.ConfigurationFileNameContract);
      m_ViewModel = _serviceLocator.GetInstance<SimulatorViewModel>();
      ConfigurationFactory = new ProducerConfigurationFactory(_configurationFileName);
      EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
      _DataGenerator = new DataGenerator();
      BindingFactory = _DataGenerator;
      MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
    }
    #endregion

    #region API
    /// <summary>
    /// Setups this instance.
    /// </summary>
    public void Setup()
    {
      try
      {
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(SimulatorDataManagementSetup)}.{nameof(Setup)} starting");
        m_ViewModel.ChangeProducerCommand(() => { m_ViewModel.ProducerErrorMessage = "Restarted"; });
        Start();
        m_ViewModel.ProducerErrorMessage = "Running";
        ReferenceApplicationEventSource.Log.Initialization($" Setup of the producer engine has been accomplished and it starts sending data.");
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.LogException(_ex);
        m_ViewModel.ProducerErrorMessage = "ERROR";
        Dispose();
      }
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (disposing)
        _DataGenerator.Dispose();
    }
    #endregion    
    
    #region private
    /// <summary>
    /// Gets or sets the view model to be used for diagnostic purpose..
    /// </summary>
    /// <value>The view model.</value>
    private SimulatorViewModel m_ViewModel;
    private DataGenerator _DataGenerator = null;
    #endregion

  }

}
