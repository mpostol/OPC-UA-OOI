
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
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
    [Import(typeof(IProducerViewModel))]
    internal IProducerViewModel ViewModel
    {
      get; set;
    }
    [Import(ProducerCompositionSettings.ConfigurationFactoryContract, typeof(IConfigurationFactory))]
    public IConfigurationFactory ProducerConfigurationFactory
    {
      set { ConfigurationFactory = value; }
    }
    [Import(ProducerCompositionSettings.EncodingFactoryContract, typeof(IEncodingFactory))]
    public IEncodingFactory ProducerEncodingFactory
    {
      set { EncodingFactory = value; }
    }
    [Import(ProducerCompositionSettings.BindingFactoryContract, typeof(IBindingFactory))]
    public IBindingFactory ProducerBindingFactory
    {
      set
      {
        BindingFactory = value;
        m_Simulator = value as IDisposable;
      }
    }
    #endregion

    #region API
    internal void Setup()
    {
      try
      {
        ReferenceApplicationEventSource.Log.Initialization($"{nameof(OPCUAServerProducerSimulator)}.{nameof(Setup)} starting");
        ViewModel.ProducerRestart += (sender, e) => Restart();
        Start();
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
    }
    #endregion

    #region private
    private IDisposable m_Simulator = null;
    private void Restart()
    {
      Debug.Assert(m_Simulator != null);
      Start();
    }
    #endregion

  }

}
