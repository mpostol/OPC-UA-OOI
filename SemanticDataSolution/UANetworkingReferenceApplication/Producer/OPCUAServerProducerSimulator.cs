
using System;
using System.Collections.Generic;
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Producer
{

  /// <summary>
  /// Class OPCUAServerProducerSimulator simulates interface to internal <see cref="CustomNodeManager"/> class.
  /// </summary>
  internal class OPCUAServerProducerSimulator : DataManagementSetup, IDisposable
  {
    #region creator
    internal static void CreateDevice(Action<IDisposable> toDispose, Action<string> trace, IProducerModelView modelView)
    {
      Current = new OPCUAServerProducerSimulator();
      toDispose(Current);
      Current.m_Trace = trace;
      Current.m_ModelView = modelView;
      Current.Setup();
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
      foreach (IDisposable _2Dispose in m_ToDispose)
        _2Dispose.Dispose();
      m_ToDispose.Clear();
    }
    #endregion

    #region API
    /// <summary>
    /// Gets the current instance of the <see cref="OPCUAServerProducerSimulator"/>.
    /// </summary>
    /// <value>The current.</value>
    public static OPCUAServerProducerSimulator Current { get; private set; }
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
    private Action<string> m_Trace;
    private IProducerModelView m_ModelView;
    private void Setup()
    {
      try
      {
        m_ModelView.ProducerRestart = new RestartCommand(Current.Restart);
        Current.ConfigurationFactory = new ProducerConfigurationFactory();
        CustomNodeManager _simulator = new CustomNodeManager();
        m_ToDispose.Add(_simulator);
        Current.BindingFactory = _simulator;
        Current.EncodingFactory = _simulator;
        Current.MessageHandlerFactory = new ProducerMessageHandlerFactory(x => m_ToDispose.Add(x), m_Trace, m_ModelView);
        Current.Initialize();
        Current.Run();
        _simulator.Run();
        m_ModelView.ProducerErrorMessage = "Running";
      }
      catch (Exception ex)
      {
        m_ModelView.ProducerErrorMessage = String.Format("Error: {0}", ex.Message);
        Dispose();
      }
    }
    private void Restart()
    {
      Dispose();
      Setup();
    }
    #endregion

  }
}
