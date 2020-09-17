//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Configuration.Networking.Serializers;
using UAOOI.Configuration.Networking.UnitTest.Instrumentation;

namespace UAOOI.Configuration.Networking.UnitTest
{
  [TestClass]
  [DeploymentItem(@"TestData\", @"TestData\")]
  public class ConfigurationFactoryBaseUnitTest
  {

    [TestMethod]
    [TestCategory("DataBindings_XmlSerializerTestMethod")]
    public void CreationStateTest()
    {
      TestConfigurationDataFactory _newOne = new TestConfigurationDataFactory();
      Assert.IsNotNull(_newOne.Loader);
      Assert.IsNull(_newOne.Configuration);
      ConfigurationData _config = _newOne.GetConfiguration();
      Assert.IsNotNull(_config);
      Assert.IsNotNull(_newOne.Configuration);
      Assert.AreSame(_config, _newOne.Configuration);
    }
    [TestMethod]
    public void LoadConfigurationDataWrapperTest()
    {
      TestConfigurationFactoryBaseConfigurationDataWrapper _newOne = new TestConfigurationFactoryBaseConfigurationDataWrapper();
      Assert.IsNotNull(_newOne.Loader);
      ConfigurationData _config = _newOne.GetConfiguration();
      Assert.IsNotNull(_config);
      Assert.IsNotNull(_newOne.Configuration);
    }
    private class TestConfigurationDataFactory : ConfigurationFactoryBase<ConfigurationData>
    {

      /// <summary>
      /// Initializes a new instance of the <see cref="TestConfigurationDataFactory"/> class.
      /// </summary>
      public TestConfigurationDataFactory()
      {
        Loader = LoadConfig;
      }

      #region ConfigurationFactoryBase
      /// <summary>
      /// Occurs after the association configuration has been changed.
      /// </summary>
      public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
      /// <summary>
      /// Occurs after the communication configuration has been changed.
      /// </summary>
      public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
      #endregion

      internal new Func<ConfigurationData> Loader { get => base.Loader; set => base.Loader = value; }

      #region private
      private ConfigurationData LoadConfig()
      {
        FileInfo _configurationFile = new FileInfo(_ConsumerConfigurationFileName);
        return ConfigurationDataFactoryIO.Load<ConfigurationData>(() => XmlDataContractSerializers.Load<ConfigurationData>(_configurationFile, (x, y, z) => { }), () => RaiseEvents());
      }
      protected override void RaiseEvents()
      {
        OnAssociationConfigurationChange?.Invoke(this, EventArgs.Empty);
        OnMessageHandlerConfigurationChange?.Invoke(this, EventArgs.Empty);
      }
      private readonly string _ConsumerConfigurationFileName = @"TestData\ConfigurationDataConsumer.xml";
      #endregion

    }
    private class TestConfigurationFactoryBaseConfigurationDataWrapper : ConfigurationFactoryBase<ConfigurationDataWrapper>
    {

      public TestConfigurationFactoryBaseConfigurationDataWrapper() : base(m_FileName) { }

      #region ConfigurationFactoryBase
      public override event EventHandler<EventArgs> OnAssociationConfigurationChange;
      public override event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
      protected override void RaiseEvents()
      {
        throw new NotImplementedException();
      }
      #endregion

      internal new Func<ConfigurationDataWrapper> Loader { get => base.Loader; set => base.Loader = value; }

      private const string m_FileName = @"TestData\ConsumerConfigurationDataWrapper.xml";

    }
  }
}
