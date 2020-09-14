//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Windows;
using UAOOI.Networking.DataRepository.DataLogger;
using UAOOI.Networking.ReferenceApplication.Core;
using UAOOI.Networking.ReferenceApplication.Core.Diagnostic;
using UAOOI.Networking.ReferenceApplication.MEF;
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
      this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Settings.Default.DataProducerProvider));
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
        IProducerDataManagementSetup m_Producer = Container.GetExportedValue<IProducerDataManagementSetup>();
        m_Producer.Setup();
        m_Components.Add(m_Producer);
        ReferenceApplicationEventSource.Log.PartCreated(nameof(IProducerDataManagementSetup));
      }
      catch (Exception _ex)
      {
        ReferenceApplicationEventSource.Log.EnteringMethod(nameof(AppBootstrapper), $"{nameof(AppBootstrapper)} at catch (Exception _ex)");
        ReferenceApplicationEventSource.Log.LogException(_ex);
        throw;
      }
    }

    #endregion MefBootstrapper

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
      if (ex == null)
        throw new ArgumentNullException(nameof(ex), $"Calling {nameof(MasterTraceException)} with null exception doesn't make sense.");
      string _innerText = inner ? "inner" : "";
      Logger.TraceData(TraceEventType.Information, 86, $"During {nameof(AppBootstrapper.Dispose)} an {_innerText} exeption been thrown: {ex.ToString()}.");
      if (ex.InnerException == null)
        return;
      MasterTraceException(ex.InnerException, true);
    }

    #endregion IDisposable

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

    #endregion private
  }
}