
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using UAOOI.SemanticData.UnitTest.Helpers;


namespace UAOOI.SemanticData.UnitTest
{
  [TestClass]
  public class AddressSpaceContextUnitTest
  {
    [TestMethod]
    public void AddressSpaceContextUnitTestTestMethod()
    {
      UANodeSet _ns = TestData.CreateNodeSetModel();
      List<TraceMessage> _trace = new List<TraceMessage>();
      int _diagnosticCounter = 0;
      Assert.IsTrue(_ns.NamespaceUris.Length >= 1, "Wrong test data - NamespaceUris must contain more then 2 items");
      AddressSpaceContext _as = new AddressSpaceContext(x => { Helpers.TraceHelper.TraceDiagnostic(x, _trace, ref _diagnosticCounter); });
      Assert.IsNotNull(_as);
      ExportModelFactoryStub _md = new ExportModelFactoryStub();
      _as.CreateInstance(_ns.NamespaceUris[0], x => x.ImportNodeSet(_ns, true), _md);
      Assert.IsNotNull(_md);
      Assert.Inconclusive();
      Assert.AreEqual<int>(0, _trace.Where<TraceMessage>(x => x.BuildError.Focus != Focus.Diagnostic).Count<TraceMessage>());
    }
    private class ExportModelFactoryStub : IExportModelFactory
    {

      public void CreateNamespace(string uri)
      {
        ;
      }
      public NodeFactory NewExportNodeFFactory<NodeFactory>()
        where NodeFactory : IExportNodeFactory
      {
        IExportNodeFactory _df = default(NodeFactory);
        if (typeof(NodeFactory).IsEquivalentTo(typeof(IExportReferenceTypeFactory)))
          _df = new ExportReferenceTypeFactory();
        else
          throw new NotImplementedException();
        return (NodeFactory)_df;
      }
      class ExportReferenceTypeFactory : IExportReferenceTypeFactory
      {
        public LocalizedText[] InverseName
        {
          set {  }
        }

        public bool Symmetric
        {
          set {  }
        }

        public XmlQualifiedName BaseType
        {
          set { }
        }

        public bool IsAbstract
        {
          set { }
        }

        public string BrowseName
        {
          set { }
        }

        public IExportInstanceFactory[] Children
        {
          get
          {
            return null;
          }
          set
          {
            
          }
        }

        public LocalizedText[] Description
        {
          get
          {
            return null;
          }
          set
          {
          }
        }

        public LocalizedText[] DisplayName
        {
          set { }
        }

        public IUAReferenceContext[] References
        {
          set { }
        }
        public XmlQualifiedName SymbolicName
        {
          get
          {
            return null;
          }
          set
          {
          }
        }
        public uint WriteAccess
        {
          set { }
        }
        public NodeFactory NewExportNodeFFactory<NodeFactory>() where NodeFactory : IExportNodeFactory
        {
          throw new NotImplementedException();
        }
      }
    }
  }
}
