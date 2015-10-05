using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class ConsumerAssociationUnitTest
  {
    [TestMethod]
    [TestCategory("DataManagement_ConsumerAssociation")]
    public void ConsumerAssociationCtorTestMethod()
    {
      ConsumerAssociation _ca = new ConsumerAssociation(new TestISemanticData(), "ConsumerAssociationCtorTestMethod", PersistentConfiguration.GetDataSet(), new IBF(), new IEF(), new IC());
      Assert.IsNotNull(_ca);
    }
    private class TestISemanticData : ISemanticData
    {
      public TestISemanticData()
        : this("SymbolicName".AddId(_count), _count)
      {
        _count++;
      }
      public TestISemanticData(string symbolicName, IComparable nodeId)
      {
        Identifier = new Uri(@"Http://commsvr.com");
        SymbolicName = symbolicName;
        NodeId = nodeId;
      }
      public Uri Identifier
      {
        get;
        private set;
      }
      public string SymbolicName
      {
        get;
        private set;
      }
      public IComparable NodeId
      {
        get;
        private set;
      }
      private static int _count = 0;
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
    private class IC : ISemanticDataItemConfiguration
    {

      public bool State
      {
        get { throw new NotImplementedException(); }
      }

      public void Enable()
      {
        throw new NotImplementedException();
      }

      public void Disable()
      {
        throw new NotImplementedException();
      }
    }
  }
}
