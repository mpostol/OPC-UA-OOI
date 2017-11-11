
using System;
using System.Windows;
using CommonServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.ReferenceApplication.MEF;

namespace UAOOI.Networking.ReferenceApplication.UnitTest.MEF
{
  [TestClass]
  public class BootstrapperBaseUnitTest
  {
    [TestMethod]
    public void LoggerDefaultsToNull()
    {
      TestBootstrapperBase _bootstrapper = new TestBootstrapperBase();
      Assert.IsNull(_bootstrapper.BaseLogger);
      Assert.IsNull(_bootstrapper.BaseShell);
      Assert.IsNull(_bootstrapper.CallCreateShell());
    }

    [TestMethod]
    public void CreateLoggerInitializesLogger()
    {
      TestBootstrapperBase _bootstrapper = new TestBootstrapperBase();
      _bootstrapper.CallCreateLogger();
      Assert.IsNotNull(_bootstrapper.BaseLogger);
      Assert.IsInstanceOfType(_bootstrapper.BaseLogger, typeof(TraceSourceBase));
    }
    [TestMethod]
    public void RegisterFrameworkExceptionTypesShouldRegisterActivationException()
    {
      TestBootstrapperBase _bootstrapper = new TestBootstrapperBase();
      _bootstrapper.CallRegisterFrameworkExceptionTypes();
      Assert.IsTrue(ExceptionExtensions.IsFrameworkExceptionRegistered(typeof(ActivationException)));
    }
    [TestMethod]
    public void OnInitializedShouldRunLast()
    {
      TestBootstrapperBase _bootstrapper = new TestBootstrapperBase();
      _bootstrapper.Run();
      Assert.IsTrue(_bootstrapper.ExtraInitialization);
    }
    private class TestBootstrapperBase : BootstrapperBase
    {

      #region BootstrapperBase
      protected override DependencyObject CreateShell()
      {
        throw new NotImplementedException();
      }
      protected override void InitializeShell()
      {
        throw new NotImplementedException();
      }
      protected override void OnInitialized()
      {
        ExtraInitialization = true;
      }
      public override void Run(bool runWithDefaultConfiguration)
      {
        Assert.IsTrue(runWithDefaultConfiguration);
        Assert.IsFalse(this.ExtraInitialization);
      }
      #endregion

      internal void CallCreateLogger()
      {
        this.Logger = base.CreateLogger();
      }
      internal void CallRegisterFrameworkExceptionTypes()
      {
        base.RegisterFrameworkExceptionTypes();
      }
      internal bool ExtraInitialization = false;
      internal DependencyObject CallCreateShell()
      {
        return base.CreateShell();
      }
      internal ITraceSource BaseLogger { get { return base.Logger; } }
      internal DependencyObject BaseShell { get { return base.Shell; } }

    }

  }
}
