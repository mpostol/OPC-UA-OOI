
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
  internal sealed class OPCUAServerProducerSimulator : DataManagementSetup, IDisposable
  {

    #region Composition
    [Import(typeof(IProducerViewModel))]
    internal IProducerViewModel ViewModel
    {
      get; set;
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
    internal void Setup()
    {
      try
      {
        ViewModel.ProducerRestart = new RestartCommand(Current.Restart);
        Current.ConfigurationFactory = new ProducerConfigurationFactory();
        CustomNodeManager _simulator = new CustomNodeManager();
        m_ToDispose.Add(_simulator);
        Current.BindingFactory = _simulator;
        Current.EncodingFactory = _simulator;
        Current.MessageHandlerFactory = new MessageHandlerFactory(x => m_ToDispose.Add(x), x => { });
        Current.Initialize();
        Current.Run();
        _simulator.Run();
        ViewModel.ProducerErrorMessage = "Running";
      }
      catch (Exception ex)
      {
        ViewModel.ProducerErrorMessage = String.Format("Error: {0}", ex.Message);
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
