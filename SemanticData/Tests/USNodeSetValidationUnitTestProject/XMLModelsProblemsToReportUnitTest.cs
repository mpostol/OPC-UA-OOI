//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.Helpers;
using UAOOI.SemanticData.UANodeSetValidation.ModelFactoryTestingFixture;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  [DeploymentItem(@"XMLModels\ProblemsToReport", @"ProblemsToReport\")]
  public class XMLModelsProblemsToReportUnitTest
  {
    //[TestMethod]
    public void ADITest()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\ADI#509\Opc.Ua.Adi.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext();
      IAddressSpaceContext addressSpace = traceContext.AddressSpaceContext;
      addressSpace.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(1, traceContext.TraceList.Count);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, traceContext.TraceList[0].BuildError.Identifier);
      traceContext.Clear();
      addressSpace.ValidateAndExportModel(new UriBuilder("http://opcfoundation.org/UA/ADI/").Uri);
      Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.DataEncoding).Count<TraceMessage>());
      Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.DataType).Count<TraceMessage>());
      Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Naming).Count<TraceMessage>());
      Assert.AreEqual<int>(24, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NodeClass).Count<TraceMessage>());
      Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NonCategorized).Count<TraceMessage>());
      Assert.AreEqual<int>(23, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Reference).Count<TraceMessage>());
      Assert.AreEqual<int>(2, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.XML).Count<TraceMessage>());
      //errors
      Assert.AreEqual<int>(48, traceContext.TraceList.Count);
      Assert.AreEqual<int>(3, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.NodeIdNotDefined.Identifier).Count<TraceMessage>());
      Assert.AreEqual<int>(5, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.UndefinedHasSubtypeTarget.Identifier).Count<TraceMessage>());
      Assert.AreEqual<int>(18, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.UndefinedHasTypeDefinition.Identifier).Count<TraceMessage>());
    }

    [TestMethod]
    public void NameInheritedFrom0Test()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\BrowseNameInheritedFrom0\BrowseNameInheritedFrom0.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext();
      IAddressSpaceContext addressSpace = traceContext.AddressSpaceContext;
      ModelFactoryTestingFixture.InformationModelFactoryBase testingModelFixture = new InformationModelFactoryBase();
      addressSpace.InformationModelFactory = testingModelFixture;
      addressSpace.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(0, traceContext.TraceList.Count);
      traceContext.Clear();
      addressSpace.ValidateAndExportModel(new UriBuilder("http://tricycleTypeV1").Uri);
      Assert.AreEqual<int>(2, traceContext.TraceList.Count);
      IEnumerable<NodeFactoryBase> nodes = testingModelFixture.Export();
      Assert.AreEqual(3, nodes.Count<NodeFactoryBase>());
      Dictionary<string, NodeFactoryBase> nodesDictionary = nodes.ToDictionary<NodeFactoryBase, string>(x => x.SymbolicName.Name);
      AddressSpaceContext asContext = addressSpace as AddressSpaceContext;
      IEnumerable<IUANodeContext> allNodes = null;
      asContext.UTValidateAndExportModel(1, x => allNodes = x);
      Assert.IsNotNull(allNodes);
      List<IUANodeContext> orphanedNodes = new List<IUANodeContext>();
      List<IUANodeContext> processedNodes = new List<IUANodeContext>();
      foreach (IUANodeContext item in allNodes)
      {
        if (!nodesDictionary.ContainsKey(item.UANode.BrowseNameQualifiedName.Name))
        {
          orphanedNodes.Add(item);
          Debug.WriteLine($"The following node has been removed from the model: {item.ToString()}");
        }
        else
          processedNodes.Add(item);
      }
      Debug.WriteLine($"The recovered information model contains {nodesDictionary.Count} nodes");
      Debug.WriteLine($"The source information model contains {allNodes.Count<IUANodeContext>()} nodes");
      Debug.WriteLine($"Number of nodes not considered for export {orphanedNodes.Count}");
      Debug.WriteLine($"Number of processed nodes {processedNodes.Count}");
    }

    [TestMethod]
    public void eoursel510Test()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\eoursel510\Opc.Ua.NodeSet2.TriCycleType_V1.1.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext();
      IAddressSpaceContext addressSpace = traceContext.AddressSpaceContext;
      ModelFactoryTestingFixture.InformationModelFactoryBase testingModelFixture = new InformationModelFactoryBase();
      addressSpace.InformationModelFactory = testingModelFixture;
      addressSpace.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(0, traceContext.TraceList.Count);
      traceContext.Clear();
      addressSpace.ValidateAndExportModel(new UriBuilder("http://tricycleTypeV1").Uri);
      Assert.AreEqual<int>(5, traceContext.TraceList.Count);
      IEnumerable<NodeFactoryBase> nodes = testingModelFixture.Export();
      Assert.AreEqual(22, nodes.Count<NodeFactoryBase>());
      Dictionary<string, NodeFactoryBase> nodesDictionary = nodes.ToDictionary<NodeFactoryBase, string>(x => x.SymbolicName.Name);
      AddressSpaceContext asContext = addressSpace as AddressSpaceContext;
      IEnumerable<IUANodeContext> allNodes = null;
      asContext.UTValidateAndExportModel(1, x => allNodes = x);
      Assert.IsNotNull(allNodes);
      List<IUANodeContext> orphanedNodes = new List<IUANodeContext>();
      List<IUANodeContext> processedNodes = new List<IUANodeContext>();
      foreach (IUANodeContext item in allNodes)
      {
        if (!nodesDictionary.ContainsKey(item.UANode.BrowseNameQualifiedName.Name))
        {
          orphanedNodes.Add(item);
          Debug.WriteLine($"The following node has been removed from the model: {item.ToString()}");
        }
        else
          processedNodes.Add(item);
      }
      Debug.WriteLine($"The recovered information model contains {nodesDictionary.Count} nodes");
      Debug.WriteLine($"The source information model contains {allNodes.Count<IUANodeContext>()} nodes");
      Debug.WriteLine($"Number of nodes not considered for export {orphanedNodes.Count}");
      Debug.WriteLine($"Number of processed nodes {processedNodes.Count}");
    }

    [TestMethod]
    public void fgolra177Test()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\fgolra177\Opc.Ua.Semi.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext();
      IAddressSpaceContext addressSpace = traceContext.AddressSpaceContext;
      addressSpace.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(0, traceContext.TraceList.Count);
      traceContext.Clear();
      addressSpace.ValidateAndExportModel(new UriBuilder("https://agileo-automation.com/UA/Semi/").Uri);
      Assert.AreEqual<int>(0, traceContext.TraceList.Count);
    }

    [TestMethod]
    public void HasOrderedComponentTest()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\HasOrderedComponent\Opc.Ua.NodeSet2.TriCycleType_V1.1.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext();
      IAddressSpaceContext addressSpace = traceContext.AddressSpaceContext;
      addressSpace.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(0, traceContext.TraceList.Count);
      traceContext.Clear();
      addressSpace.ValidateAndExportModel(new UriBuilder("http://tricycleTypeV1").Uri);
      Assert.AreEqual<int>(0, traceContext.TraceList.Count);
    }

    //[TestMethod]
    public void MachineVisionTest()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\MachineVision\Opc.Ua.MachineVision.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext();
      IAddressSpaceContext addressSpace = traceContext.AddressSpaceContext;
      addressSpace.ImportUANodeSet(_testDataFileInfo);
      Assert.AreEqual<int>(0, traceContext.TraceList.Count);
      traceContext.Clear();
      addressSpace.ValidateAndExportModel(new UriBuilder("http://opcfoundation.org/UA/MachineVision").Uri);
      Assert.AreEqual<int>(2, traceContext.TraceList.Count);
      Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, traceContext.TraceList[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, traceContext.TraceList[1].BuildError.Identifier);
    }
  }
}