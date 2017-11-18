
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using UAOOI.Networking.SemanticData;
using UAOOI.Networking.UDPMessageHandler;

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
        ViewModel.ProducerRestart = new RestartCommand(Restart);
        ConfigurationFactory = new ProducerConfigurationFactory();
        MessageHandlerFactory = new MessageHandlerFactory(x => { });
        BindAndStartRunning();
        ViewModel.ProducerErrorMessage = "Running";
      }
      catch (Exception ex)
      {
        ViewModel.ProducerErrorMessage = String.Format("Error: {0}", ex.Message);
        Dispose();
      }
    }
    #endregion

    #region IDisposable
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      foreach (IDisposable _toDispose in m_ToDispose)
        _toDispose.Dispose();
    } 
    #endregion

    #region private
    private class RestartCommand : ICommand
    {
      public RestartCommand(Action restart)
      {
        m_restart = restart;
      }
      public event EventHandler CanExecuteChanged;
      public bool CanExecute(object parameter)
      {
        return true;
      }
      public void Execute(object parameter)
      {
        m_restart();
      }
      private Action m_restart;
    }
    private List<IDisposable> m_ToDispose = new List<IDisposable>();
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
