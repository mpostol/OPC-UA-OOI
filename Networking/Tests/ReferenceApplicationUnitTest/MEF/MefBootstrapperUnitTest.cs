using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.ReferenceApplication.MEF;

namespace UAOOI.Networking.ReferenceApplication.UnitTest.MEF
{
  [TestClass]
  public class MefBootstrapperUnitTest
  {

    [TestMethod]
    public void TestMefBootstrapperConstructor()
    {

      TestMefBootstrapper _newMefBootstrapper = new TestMefBootstrapper();
      _newMefBootstrapper.TestStartupState();
      _newMefBootstrapper.Run();
      _newMefBootstrapper.TestAfterRunState();
    }
    private class TestMefBootstrapper : MefBootstrapper
    {

      protected override void ConfigureAggregateCatalog()
      {
        m_CallSequence.Add(nameof(ConfigureAggregateCatalog));
        base.ConfigureAggregateCatalog();
      }
      protected override void ConfigureContainer()
      {
        m_CallSequence.Add(nameof(ConfigureContainer));
        base.ConfigureContainer();
      }
      protected override AggregateCatalog CreateAggregateCatalog()
      {
        m_CallSequence.Add(nameof(CreateAggregateCatalog));
        return base.CreateAggregateCatalog();
      }
      protected override CompositionContainer CreateContainer()
      {
        m_CallSequence.Add(nameof(CreateContainer));
        return base.CreateContainer();
      }
      protected override ITraceSource CreateLogger()
      {
        m_CallSequence.Add(nameof(CreateLogger));
        return base.CreateLogger();
      }
      protected override DependencyObject CreateShell()
      {
        m_CallSequence.Add(nameof(CreateShell));
        return base.CreateShell();
      }
      protected override void InitializeShell()
      {
        m_CallSequence.Add(nameof(InitializeShell));
        base.InitializeShell();
      }
      protected override void OnInitialized()
      {
        m_CallSequence.Add(nameof(OnInitialized));
        m_ExternalInitialization = true;
      }
      protected override void RegisterBootstrapperProvidedTypes()
      {
        m_CallSequence.Add(nameof(RegisterBootstrapperProvidedTypes));
        base.RegisterBootstrapperProvidedTypes();
      }
      public override void RegisterDefaultTypesIfMissing()
      {
        m_CallSequence.Add(nameof(RegisterDefaultTypesIfMissing));
        base.RegisterDefaultTypesIfMissing();
      }
      protected override void RegisterFrameworkExceptionTypes()
      {
        m_CallSequence.Add(nameof(RegisterFrameworkExceptionTypes));
        base.RegisterFrameworkExceptionTypes();
      }
      public override void Run(bool runWithDefaultConfiguration)
      {
        m_CallSequence.Add(nameof(Run));
        base.Run(runWithDefaultConfiguration);
      }

      #region UT instrumentation
      internal void TestStartupState()
      {
        Assert.IsNull(this.AggregateCatalog);
        Assert.IsNull(this.Container);
        Assert.IsNull(this.Logger);
        Assert.IsNull(this.Shell);
        Assert.IsFalse(m_ExternalInitialization);
        Assert.AreEqual<int>(0, m_CallSequence.Count);
      }
      internal void TestAfterRunState()
      {
        Assert.IsTrue(m_ExternalInitialization);
        Assert.IsNotNull(this.AggregateCatalog);
        Assert.IsNotNull(this.Container);
        Assert.IsNotNull(this.Logger);
        Assert.IsNull(this.Shell);
        Assert.AreEqual<int>(12, m_CallSequence.Count);
        int i = 0;
        Assert.AreEqual<string>(nameof(Run), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(CreateLogger), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(CreateAggregateCatalog), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(ConfigureAggregateCatalog), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(RegisterDefaultTypesIfMissing), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(CreateContainer), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(ConfigureContainer), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(RegisterBootstrapperProvidedTypes), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(RegisterFrameworkExceptionTypes), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(CreateShell), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(InitializeShell), m_CallSequence[i++]);
        Assert.AreEqual<string>(nameof(OnInitialized), m_CallSequence[i++]);
        MainWindow _MainWindow = this.Container.GetExportedValue<MainWindow>();
        Assert.IsNotNull(_MainWindow);
      }
      private bool m_ExternalInitialization = false;
      private List<string> m_CallSequence = new List<string>();
      #endregion

    }

  }
}
