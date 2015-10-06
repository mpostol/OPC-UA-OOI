using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ProducerAssociationUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_ProducerAssociation")]
    [ExpectedException(typeof(NotImplementedException))]
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
    }
    private class IBF : IBindingFactory
    {

      public IBinding GetBinding(string repositoryGroup, string variableName)
      {
        return new MyBinding();
      }
      private class MyBinding : IBinding
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
