//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UAModelDesignExport.Instrumentation;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  public class NodeFactoryBaseUnitTest
  {
    [TestMethod]
    public void CreatorTestMethod()
    {
      NodeFactory _nf = new NodeFactory(x => { });
      Assert.IsNotNull(_nf);
    }
    [TestMethod]
    public void DescriptionTestMethod()
    {
      int _counter = 0;
      NodeFactory _nf = new NodeFactory(x => _counter++);
      Assert.AreEqual<int>(0, _counter);
      LocalizedText _lt = new LocalizedText() { Key = "localeField", Value = "valueField" };
      _nf.AddDescription(_lt.Key, _lt.Value);
      Assert.AreEqual<int>(0, _counter);
      _nf.AddDescription(_lt.Key, _lt.Value);
      Assert.AreEqual<int>(1, _counter);
      _nf.AddDescription(_lt.Key, _lt.Value);
      Assert.AreEqual<int>(2, _counter);
      List<string> _path = new List<string>();
      NodeDesign _nd = _nf.Export(_path, (x, Y) => { });
      _lt.Compare(_nd.Description);
    }
    [TestMethod]
    public void DisplayNameTestMethod()
    {
      int _counter = 0;
      NodeFactory _nf = new NodeFactory(x => _counter++);
      Assert.AreEqual<int>(0, _counter);
      LocalizedText _lt = new LocalizedText() { Key = "localeField", Value = "valueField" };
      _nf.AddDisplayName(_lt.Key, _lt.Value);
      Assert.AreEqual<int>(0, _counter);
      _nf.AddDisplayName(_lt.Key, _lt.Value);
      Assert.AreEqual<int>(1, _counter);
      _nf.AddDisplayName(_lt.Key, _lt.Value);
      Assert.AreEqual<int>(2, _counter);
      List<string> _path = new List<string>();
      NodeDesign _nd = _nf.Export(_path, (x, Y) => { });
      _lt.Compare(_nd.DisplayName);
    }
    [TestMethod]
    public void NodeFactoryExportTest()
    {
      int _counter = 0;
      NodeFactory _nf = new NodeFactory(x => _counter++);
      _nf.AccessRestrictions = AccessRestrictions.EncryptionRequired | AccessRestrictions.SessionRequired | AccessRestrictions.SigningRequired;
      _nf.BrowseName = BrowseNames.AggregateFunction_MinimumActualTime.ToString();
      _nf.Category = new string[] { "cat1", "cat2" };
      _nf.DataTypePurpose = InformationModelFactory.DataTypePurpose.ServicesOnly;
      _nf.ReleaseStatus = InformationModelFactory.ReleaseStatus.Draft;
      _nf.SymbolicName = new XmlQualifiedName("name", "ns");
      List<string> _path = new List<string>();
      NodeDesign _md = _nf.Export(_path, (x, Y) => { });
      Assert.AreEqual<string>(BrowseNames.AggregateFunction_MinimumActualTime.ToString(), _md.BrowseName);
      Assert.AreEqual<string>("cat1, cat2", _md.Category);
      Assert.IsNull(_md.Children);
      Assert.IsFalse(_md.NumericIdSpecified);
      Assert.AreEqual<uint>(0, _md.PartNo);  //is not copied form the UANodeSet; 
      Assert.AreEqual<XML.DataTypePurpose>(DataTypePurpose.ServicesOnly, _md.Purpose);
      Assert.AreEqual<XML.ReleaseStatus>(XML.ReleaseStatus.Draft, _md.ReleaseStatus);
      Assert.AreEqual<XmlQualifiedName>(new XmlQualifiedName("name", "ns"), _md.SymbolicName);
    }
    private class NodeFactory : NodeFactoryBase
    {
      public NodeFactory(Action<TraceMessage> traceEvent)
        : base(traceEvent)
      {
        SymbolicName = new System.Xml.XmlQualifiedName( "Name", "NameSpace");
      }
      internal override NodeDesign Export(List<string> path, Action<InstanceDesign, List<string>> createInstanceType)
      {
        NodeDesign _nd = new NodeDesign() {  };
        base.UpdateNode(_nd, path, createInstanceType);
        return _nd;
      }
    }
  }
}
