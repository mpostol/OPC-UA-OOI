//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.UnitTest
{

  [TestClass]
  public class UAModelContextUnitTest
  {
    [TestMethod]
    [TestCategory("Code")]
    public void ConstructorTest()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      IAddressSpaceBuildContext _as = new AddressSpaceFixture();
      UAModelContext _mc = new UAModelContext(_tm, _as);
    }
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentNullException))]
    [TestCategory("Code")]
    public void CreateUAModelContextNodeAliasNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      _tm.Aliases = null;
      IAddressSpaceBuildContext _as = new AddressSpaceFixture();
      UAModelContext _mc = new UAModelContext(_tm, _as);
    }
    [TestMethod]
    [TestCategory("Code")]
    [ExpectedException(typeof(System.ArgumentNullException))]
    public void CreateUAModelContextAddressSpaceContextNull()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      UAModelContext _mc = new UAModelContext(_tm, null);
    }
    [TestMethod]
    [TestCategory("Code")]
    public void CreateUAModelContextModelNamespaceUrisNullTest()
    {
      List<TraceMessage> _log = new List<TraceMessage>();
      BuildErrorsHandling.Log.TraceEventAction += _msg => _log.Add(_msg);
      UANodeSet _tm = TestData.CreateNodeSetModel();
      _tm.NamespaceUris = null;
      AddressSpaceFixture _as = new AddressSpaceFixture();
      UAModelContext _mc = new UAModelContext(_tm, _as);
      List<NodeId> nodeIdList = new List<NodeId>();
      foreach (UANode _nd in _tm.Items)
        nodeIdList.Add(_mc.ImportNodeId(_nd.NodeId));
      BuildErrorsHandling.Log.TraceEventAction -= _msg => _log.Add(_msg);
      Assert.AreEqual<int>(1, _log.Count);
      Assert.AreEqual<string>("P3-0802020000", _log[0].BuildError.Identifier);
      Assert.AreEqual<int>(1, nodeIdList.Count);
      Assert.AreEqual<int>(0, _as.m_NamespaceTable.GetIndex(Namespaces.OpcUa));
      Assert.AreEqual<int>(-1, _as.m_NamespaceTable.GetIndex(@"NameUnknown0"));
    }
    [TestMethod]
    [TestCategory("Code")]
    public void ImportNodeIdTest()
    {
      List<TraceMessage> _log = new List<TraceMessage>();
      BuildErrorsHandling.Log.TraceEventAction += _msg => _log.Add(_msg);
      UANodeSet _tm = TestData.CreateNodeSetModel();
      AddressSpaceFixture _as = new AddressSpaceFixture();
      UAModelContext _mc = new UAModelContext(_tm, _as);
      List<NodeId> nodeIdList = new List<NodeId>();
      foreach (UANode _nd in _tm.Items)
        nodeIdList.Add(_mc.ImportNodeId(_nd.NodeId));
      BuildErrorsHandling.Log.TraceEventAction -= _msg => _log.Add(_msg);
      Assert.AreEqual<int>(0, _log.Count);
      Assert.AreEqual<int>(1, nodeIdList.Count);
      Assert.AreEqual<int>(0, _as.m_NamespaceTable.GetIndex(Namespaces.OpcUa));
      Assert.AreEqual<int>(1, _as.m_NamespaceTable.GetIndex(@"http://cas.eu/UA/Demo/"));
    }
    [TestMethod]
    public void ExportBrowseNameTest()
    {
      UANodeSet _tm = TestData.CreateNodeSetModel();
      IAddressSpaceBuildContext _as = new AddressSpaceFixture();
      UAModelContext _mc = new UAModelContext(_tm, _as);
      XmlQualifiedName _resolvedName = _mc.ExportBrowseName(null, null);
      Assert.IsNull(_resolvedName);
      _resolvedName = _mc.ExportBrowseName(null, UAInformationModel.DataTypes.BaseDataType);
      Assert.IsNull(_resolvedName);
      _resolvedName = _mc.ExportBrowseName(new NodeId(UAInformationModel.DataTypes.BaseDataType, 0).ToString(), UAInformationModel.DataTypes.BaseDataType);
      Assert.IsNull(_resolvedName);
      _resolvedName = _mc.ExportBrowseName(new NodeId(UAInformationModel.DataTypes.Structure, 0).ToString(), UAInformationModel.DataTypes.BaseDataType);
      Assert.IsNotNull(_resolvedName);
      Assert.AreEqual(@"http://opcfoundation.org/UA/:Structure", _resolvedName.ToString());
    }
    private class AddressSpaceFixture : IAddressSpaceBuildContext
    {
      public Parameter ExportArgument(Argument argument, XmlQualifiedName dataType)
      {
        throw new System.NotImplementedException();
      }
      public XmlQualifiedName ExportBrowseName(NodeId id)
      {
        Assert.IsNotNull(id);
        Assert.AreEqual<int>(0, id.NamespaceIndex);
        switch ((uint)id.IdentifierPart)
        {
          case 22:
            return new XmlQualifiedName(BrowseNames.Structure, Namespaces.OpcUa);
          default:
            Assert.Fail();
            break;
        }
        return null;
      }
      public void GetDerivedInstances(IUANodeContext rootNode, List<IUANodeBase> list)
      {
        throw new System.NotImplementedException();
      }
      public ushort GetIndexOrAppend(string identifier)
      {
        return m_NamespaceTable.GetIndexOrAppend(identifier);
      }
      public NamespaceTable m_NamespaceTable = new NamespaceTable();
      public string GetNamespace(ushort namespaceIndex)
      {
        throw new System.NotImplementedException();
      }
      public IUANodeContext GetOrCreateNodeContext(NodeId id, IUAModelContext uAModelContext)
      {
        throw new System.NotImplementedException();
      }
      public IEnumerable<UAReferenceContext> GetReferences2Me(IUANodeContext index)
      {
        throw new System.NotImplementedException();
      }
      public IEnumerable<UAReferenceContext> GetMyReferences(IUANodeBase index)
      {
        throw new System.NotImplementedException();
      }

      public IUANodeBase GetBaseTypeNode(NodeClassEnum nodeClass)
      {
        throw new System.NotImplementedException();
      }

    }
  }
}
