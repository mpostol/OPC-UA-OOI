
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
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
    private IDisposable m_Simulator = null;
    private void Restart()
    {
      Debug.Assert(m_Simulator != null);
      m_Simulator.Dispose();
      BindAndStartRunning();
    }
    private void BindAndStartRunning()
    {
      string _repositoryGroup = "repositoryGroup";
      ConfigurationFactory = new ProducerConfigurationFactory(Properties.Settings.Default.ProducerConfigurationFileName);
      IBindingFactory _simulator = new SimulatorInteroperabilityTest.DataGenerator(_repositoryGroup);
      m_Simulator = _simulator as IDisposable;
      BindingFactory = _simulator;
      EncodingFactory = new SimulatorInteroperabilityTest.EncodingFactoryBinarySimple(_repositoryGroup);
      Start();
    }
    #endregion

  }

}
