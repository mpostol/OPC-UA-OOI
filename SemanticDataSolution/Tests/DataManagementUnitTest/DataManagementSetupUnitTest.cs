
using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.Configuration;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class DataManagementSetupUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    public void DataManagementSetupCreatorTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      Assert.IsNull(_ndm.BindingFactory);
      Assert.IsNull(_ndm.ConfigurationFactory);
      Assert.IsNull(_ndm.EncodingFactory);
      Assert.IsNull(_ndm.MessageHandlerFactory);
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    public void InitializeTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.BindingFactory = new BF();
      _ndm.ConfigurationFactory = new CF();
      _ndm.EncodingFactory = new EF();
      _ndm.MessageHandlerFactory = new MF();
      _ndm.Initialize();
    }
    [TestMethod]
    [TestCategory("DataManagement_DataManagementSetup")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RunTestMethod()
    {
      DataManagementSetup _ndm = new DataManagementSetup();
      Assert.IsNotNull(_ndm);
      _ndm.Run();
    }
    private class MF : IMessageHandlerFactory
    {
      public IMessageReader GetIMessageReader(string name, XmlElement configuration)
      {
        return new MR();
      }
      public IMessageWriter GetIMessageWriter(string name, XmlElement configuration)
      {
        throw new NotImplementedException();
      }
      private class MR: IMessageReader
      {


        public IAssociationState State
        {
          get { throw new NotImplementedException(); }
        }

        public void AttachToNetwork()
        {
          throw new NotImplementedException();
        }

        public event EventHandler<MessageEventArg> ReadMessageCompleted;
      }
    }
    private class EF : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        converter.Converter = null;
        converter.Culture = null;
        converter.Parameter = null;
        Assert.IsNotNull(converter.TargetType);
      }
    }
    private class CF : IConfigurationFactory
    {
      public ConfigurationData GetConfiguration()
      {
        return PersistentConfiguration.GetLocalConfiguration();
      }
      public event EventHandler<EventArgs> OnAssociationConfigurationChange;
      public event EventHandler<EventArgs> OnMessageHandlerConfigurationChange;
    }
    private class BF : IBindingFactory
    {
      public IBinding GetBinding(string repositoryGroup, string variableName)
      {
        return new Binding();
      }
      private class Binding : IBinding
      {
        public System.Windows.Data.IValueConverter Converter
        {
          set { }
        }
        public Type TargetType
        {
          get { return this.GetType(); }
        }
        public object Parameter
        {
          set {  }
        }
        public System.Globalization.CultureInfo Culture
        {
          set {  }
        }
        public void Assign2Repository(object value)
        {
          throw new NotImplementedException();
        }
        public void OnEnabling()
        {
          throw new NotImplementedException();
        }
        public void OnDisabling()
        {
          throw new NotImplementedException();
        }
      }
    }

  }

}
