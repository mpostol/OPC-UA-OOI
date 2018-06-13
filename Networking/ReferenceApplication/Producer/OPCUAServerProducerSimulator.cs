
using System;
using System.ComponentModel.Composition;
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
    [Import(typeof(IProducerViewModel))]
    internal IProducerViewModel ViewModel
    {
      get; set;
    }
    #endregion

    #region private
    internal void Setup()
    {
      try
      {
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(OPCUAServerProducerSimulator)}.{nameof(Setup)} starting");
        ViewModel.ProducerRestart += (sender, e) => Restart();
        ConfigurationFactory = new ProducerConfigurationFactory();
        BindAndStartRunning();
        ViewModel.ProducerErrorMessage = "Running";
        ReferenceApplicationEventSource.Log.Initialization($" producer engine and starting sending data acomplished");
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.LogException(_ex);
        ViewModel.ProducerErrorMessage = "ERROR";
        Dispose();
      }
    }
    #endregion

    #region IDisposable
    protected override void Dispose(bool disposing)
    {
      ReferenceApplicationEventSource.Log.EnteringDispose(nameof(OPCUAServerProducerSimulator), disposing);
      base.Dispose(disposing);
      if (!disposing)
        return;
      m_Simulator?.Dispose();
      m_Simulator = null;
    }
    #endregion

    #region private
    private CustomNodeManager m_Simulator = null;
    private void Restart()
    {
      System.Diagnostics.Debug.Assert(m_Simulator != null);
      m_Simulator.Dispose();
      BindAndStartRunning();
    }
    private void BindAndStartRunning()
    {
      m_Simulator = new CustomNodeManager();
      BindingFactory = m_Simulator;
      EncodingFactory = m_Simulator;
      Start();
      m_Simulator.Run();
    }
    #endregion

  }

}
