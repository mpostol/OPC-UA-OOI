
using System;
using System.Collections.Generic;
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.Consumer
{
  internal sealed class ConsumerDataManagementSetup : DataManagementSetup, IDisposable
  {

    #region creator of the ConsumerDeviceSimulator
    /// <summary>
    /// Creates the device (e.g. HMI) simulator.
    /// </summary>
    /// <param name="bindingFactory">The binding factory.</param>
    /// <param name="toDispose">
    /// To dispose captures functionality to create a collection of disposable objects. 
    /// The objects are disposed when application exits.
    /// </param>
    internal static void CreateDevice(IConsumerModelView modelView, Action<IDisposable> toDispose)
    {
      Current = new ConsumerDataManagementSetup();
      toDispose(Current);
      Current.m_ModelView = modelView;
      modelView.ConsumerUpdateConfiguration = new RestartCommand(Current.Restart);
      Current.Setup();
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
      m_ModelView.Trace("Entering Dispose");
      foreach (IDisposable _2Dispose in m_ToDispose)
        _2Dispose.Dispose();
      m_ToDispose.Clear();
    }
    #endregion

    #region API
    /// <summary>
    /// Singleton implementation - gets the current instance of this class.
    /// </summary>
    /// <value>The current.</value>
    internal static ConsumerDataManagementSetup Current { get; private set; }
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
    private IConsumerModelView m_ModelView;
    private void Setup()
    {
      try
      {
        m_ModelView.Trace("Entering Setup");
        Current.ConfigurationFactory = new ConsumerConfigurationFactory();
        MainWindowModel _model = new MainWindowModel() { ModelViewBindingFactory = m_ModelView };
        Current.BindingFactory = _model;
        Current.EncodingFactory = _model;
        Current.MessageHandlerFactory = new ConsumerMessageHandlerFactory(x => m_ToDispose.Add(x), m_ModelView, m_ModelView.Trace);
        m_ModelView.Trace("Initialize consumer engine.");
        Current.Initialize();
        m_ModelView.Trace("On start receiving UDP frames.");
        Current.Run();
        m_ModelView.ConsumerErrorMessage = "Running";
      }
      catch (Exception ex)
      {
        m_ModelView.ConsumerErrorMessage = String.Format("Error: {0}", ex.Message);
        Dispose();
      }
    }
    private void Restart()
    {
      m_ModelView.Trace("Entering Restart");
      Dispose();
      Setup();
    }
    #endregion

  }
}
