
using System.Windows;
using CommonServiceLocator;
using UAOOI.Common.Infrastructure.Diagnostic;

namespace UAOOI.Networking.ReferenceApplication.MEF
{

  /// <summary>
  /// Base class that provides a basic bootstrapping sequence and hooks that specific implementations can override.
  /// </summary>
  /// <remarks>
  /// This class must be overridden to provide application specific configuration.
  /// </remarks>
  public abstract class BootstrapperBase
  {

    /// <summary>
    /// Runs the bootstrapper process.
    /// </summary>
    public void Run()
    {
      this.Run(true);
      this.OnInitialized();
    }
    /// <summary>
    /// Run the bootstrapper process.
    /// </summary>
    /// <param name="runWithDefaultConfiguration">If <c>true</c> registers default Library services in the container. This is the default behavior.
    /// </param>
    public abstract void Run(bool runWithDefaultConfiguration);
    /// <summary>
    /// Gets the current <see cref="ITraceSource"/> for the application.
    /// </summary>
    /// <value>A <see cref="ITraceSource"/> instance.</value>
    protected ITraceSource Logger { get; set; } = null;
    /// <summary>
    /// Gets the shell user interface
    /// </summary>
    /// <value>The shell user interface.</value>
    protected DependencyObject Shell { get; set; } = null;
    /// <summary>
    /// Create the <see cref="ITraceSource" /> used by the bootstrapper.
    /// </summary>
    /// <remarks>
    /// The base implementation returns a new <see cref="TraceSourceBase"/>.
    /// </remarks>
    protected virtual ITraceSource CreateLogger()
    {
      return new TraceSourceBase();
    }
    /// <summary>
    /// Registers the <see cref="System.Type"/>s of the Exceptions that are not considered 
    /// root exceptions by the <see cref="ExceptionExtensions"/>.
    /// </summary>
    protected virtual void RegisterFrameworkExceptionTypes()
    {
      ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ActivationException));
    }
    /// <summary>
    /// Creates the shell or main window of the application.
    /// </summary>
    /// <returns>The shell of the application.</returns>
    /// <remarks>
    /// If the returned instance is a <see cref="DependencyObject"/>, the
    /// <see cref="Bootstrapper"/> will attach the default <see cref="IRegionManager"/> of
    /// the application in its <see cref="RegionManager.RegionManagerProperty"/> attached property
    /// in order to be able to add regions by using the <see cref="RegionManager.RegionNameProperty"/>
    /// attached property from XAML.
    /// </remarks>
    protected virtual DependencyObject CreateShell()
    {
      return null;
    }
    /// <summary>
    /// Initializes the shell.
    /// </summary>
    protected virtual void InitializeShell() { }
    /// <summary>
    /// Contains actions that should occur last.
    /// </summary>
    protected virtual void OnInitialized() { }

  }
}

