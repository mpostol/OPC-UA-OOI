
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerAssociationUnitTest
  {

    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociation")]
    public void ProducerAssociationCreatorTestMethod()
    {
      ProducerAssociation _npa = new ProducerAssociation(new SD(), "DataManagement_ProducerAssociation", PersistentConfiguration.GetDataSet(), new IBF(), new IEF());
      Assert.IsNotNull(_npa);
    }
    private class SD : ISemanticData
    {
      public Uri Identifier
      {
        get { throw new NotImplementedException(); }
      }
      public string SymbolicName
      {
        get { throw new NotImplementedException(); }
      }
      public IComparable NodeId
      {
        get { throw new NotImplementedException(); }
      }
      public Guid Guid
      {
        get { return Guid.NewGuid(); }
      }
    }
    private class IBF : IBindingFactory
    {

      public IConsumerBinding GetConsumerBinding(string repositoryGroup, string variableName)
      {
        return new MyBinding();
      }
      public IProducerBinding GetProducerBinding(string repositoryGroup, string variableName)
      {
        string _key = String.Format("{0}.{1}", repositoryGroup, variableName);
        IProducerBinding _npb = new ProducerBindingMonitoredValue<object>(_key);
        m_Repository.Add(_key, _npb);
        return _npb;
      }

      private class MyBinding : IConsumerBinding
      {
        public System.Windows.Data.IValueConverter Converter
        {
          set { }
        }
        public Type TargetType
        {
          get { throw new NotImplementedException(); }
        }
        public object Parameter
        {
          set { }
        }
        public System.Globalization.CultureInfo Culture
        {
          set { }
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
      private Dictionary<string, IProducerBinding> m_Repository = new Dictionary<string, IProducerBinding>();
      public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
    private class IEF : IEncodingFactory
    {
      public void UpdateValueConverter(IBinding converter, string repositoryGroup, string sourceEncoding)
      {
        converter.Culture = null;
        converter.Converter = null;
        converter.Parameter = null;
      }
    }
  }
}


