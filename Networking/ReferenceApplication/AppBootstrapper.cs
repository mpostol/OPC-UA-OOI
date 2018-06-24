
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Windows;
using UAOOI.Networking.DataLogger;
using UAOOI.Networking.ReferenceApplication.Core.Diagnostic;
using UAOOI.Networking.ReferenceApplication.MEF;
using UAOOI.Networking.ReferenceApplication.Producer;
using UAOOI.Networking.ReferenceApplication.Properties;

namespace UAOOI.Networking.ReferenceApplication
{
  internal class AppBootstrapper : MefBootstrapper
  {

    #region MefBootstrapper
    /// <summary>
    /// Run the bootstrapper process.
    /// </summary>
    /// <param name="runWithDefaultConfiguration">if set to <c>true</c> run with default configuration.</param>
    public override void Run(bool runWithDefaultConfiguration)
    {
      base.Run(runWithDefaultConfiguration);
    }
    protected override void ConfigureContainer()
    {
      base.ConfigureContainer();
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Settings.Default.MessageHandlerProvider));
    }
    /// <summary>
    /// Initializes the shell.
    /// </summary>
    /// <remarks>The base implementation ensures the shell is composed in the container.</remarks>
    protected override void InitializeShell()
    {
      base.InitializeShell();
      Application.Current.MainWindow = (MainWindow)this.Shell;
      Application.Current.MainWindow.Show();
    }

    /// <summary>
    /// Creates the shell or main window of the application.
    /// </summary>
    /// <returns>The shell of the application as an exported instance of the <see cref="MainWindow" /></returns>
    protected override DependencyObject CreateShell()
    {
      return this.Container.GetExportedValue<MainWindow>();
    }
    protected override void OnInitialized()
    {
      try
      {
        base.OnInitialized();
        m_EventSourceBootstrapper = Container.GetExportedValue<EventSourceBootstrapper>();
        m_EventSourceBootstrapper.Run(EventEntry => m_Log.Add(EventEntry));
        ReferenceApplicationEventSource.Log.StartingApplication(Settings.Default.MessageHandlerProvider);
        LoggerManagementSetup m_ConsumerConfigurationFactory = Container.GetExportedValue<LoggerManagementSetup>();
        m_ConsumerConfigurationFactory.Setup();
        m_Components.Add(m_ConsumerConfigurationFactory);
        ReferenceApplicationEventSource.Log.PartCreated(nameof(LoggerManagementSetup));
        OPCUAServerProducerSimulator m_OPCUAServerProducerSimulator = Container.GetExportedValue<OPCUAServerProducerSimulator>();
        m_OPCUAServerProducerSimulator.Setup();
        m_Components.Add(m_OPCUAServerProducerSimulator);
        ReferenceApplicationEventSource.Log.PartCreated(nameof(OPCUAServerProducerSimulator));
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.EnteringMethod(nameof(AppBootstrapper), $"{nameof(AppBootstrapper)} at catch (Exception _ex)");
        ReferenceApplicationEventSource.Log.LogException(_ex);
        throw;
      }
    }
    #endregion

    #region IDisposable
    protected override void Dispose(bool disposing)
    {
      try
      {
        ReferenceApplicationEventSource.Log.EnteringDispose(nameof(AppBootstrapper.Dispose), disposing);
        m_Components.Dispose();
        base.Dispose(disposing);
        //to keep logging it must be disposed as the last operation.
        m_EventSourceBootstrapper.Dispose();
        Logger.TraceData(TraceEventType.Information, 86, $"{nameof(AppBootstrapper.Dispose)} has been accomplished successfully.");
      }
      catch (Exception _ex)
      {
        MasterTraceException(_ex, false);
      }
    }
    private void MasterTraceException(Exception ex, bool inner)
    {
      string _innerText = inner ? "inner" : "";
      Logger.TraceData(TraceEventType.Information, 86, $"During {nameof(AppBootstrapper.Dispose)} an {_innerText} exeption been thrown: {ex.ToString()}.");
      if (ex.InnerException != null)
        return;
      MasterTraceException(ex.InnerException, true);
    }
    #endregion

    #region private 
    private List<EventEntry> m_Log = new List<EventEntry>();
    private CompositeDisposable m_Components = new CompositeDisposable();
    private EventSourceBootstrapper m_EventSourceBootstrapper;
    private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      ReferenceApplicationEventSource.Log.EnteringMethod(nameof(AppBootstrapper), nameof(AppDomainUnhandledException));
      Exception _ex = e.ExceptionObject as Exception;
      ReferenceApplicationEventSource.Log.LogException(_ex);
      HandleException(_ex);
    }
    private static void HandleException(Exception ex)
    {
      if (ex == null)
        return;
      MessageBox.Show(Properties.Resources.UnhandledException, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
      Environment.Exit(1);
    }
    #endregion

  }
}
