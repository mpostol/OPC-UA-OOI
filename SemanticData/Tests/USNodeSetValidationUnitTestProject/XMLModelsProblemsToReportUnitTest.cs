//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UAOOI.SemanticData.AddressSpace.Abstractions;
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
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext(@"ProblemsToReport\ADI#509\Opc.Ua.Adi.NodeSet2.xml");
      traceContext.TestConsistency(1);
      Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, traceContext[0].BuildError.Identifier);
      traceContext.Clear();
      traceContext.ValidateAndExportModel(new UriBuilder("http://opcfoundation.org/UA/ADI/").Uri);
      traceContext.TestConsistency(48);
      Assert.AreEqual<int>(0, traceContext.Where(x => x.BuildError.Focus == Focus.DataEncoding));
      Assert.AreEqual<int>(0, traceContext.Where(x => x.BuildError.Focus == Focus.DataType));
      Assert.AreEqual<int>(0, traceContext.Where(x => x.BuildError.Focus == Focus.Naming));
      Assert.AreEqual<int>(24, traceContext.Where(x => x.BuildError.Focus == Focus.NodeClass));
      Assert.AreEqual<int>(0, traceContext.Where(x => x.BuildError.Focus == Focus.NonCategorized));
      Assert.AreEqual<int>(23, traceContext.Where(x => x.BuildError.Focus == Focus.Reference));
      Assert.AreEqual<int>(2, traceContext.Where(x => x.BuildError.Focus == Focus.XML));
      //errors
      Assert.AreEqual<int>(3, traceContext.Where(x => x.BuildError.Identifier == BuildError.NodeIdNotDefined.Identifier));
      Assert.AreEqual<int>(5, traceContext.Where(x => x.BuildError.Identifier == BuildError.UndefinedHasSubtypeTarget.Identifier));
      Assert.AreEqual<int>(18, traceContext.Where(x => x.BuildError.Identifier == BuildError.UndefinedHasTypeDefinition.Identifier));
    }

    [TestMethod]
    public void NameInheritedFrom0Test()
    {
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext(@"ProblemsToReport\BrowseNameInheritedFrom0\BrowseNameInheritedFrom0.xml");
      IAddressSpaceContext addressSpace = traceContext.AddressSpace;
      InformationModelFactoryBase testingModelFixture = new InformationModelFactoryBase();
      traceContext.TestConsistency(0);
      traceContext.Clear();
      traceContext.ValidateAndExportModel(new UriBuilder("http://tricycleTypeV1").Uri, testingModelFixture);
      traceContext.TestConsistency(2);
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
        if (!nodesDictionary.ContainsKey(item.UANode.BrowseName.Name))
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
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext(@"ProblemsToReport\eoursel510\Opc.Ua.NodeSet2.TriCycleType_V1.1.xml");
      IAddressSpaceContext addressSpace = traceContext.AddressSpace;
      ModelFactoryTestingFixture.InformationModelFactoryBase testingModelFixture = new InformationModelFactoryBase();
      traceContext.TestConsistency(0);
      traceContext.Clear();
      traceContext.ValidateAndExportModel(new UriBuilder("http://tricycleTypeV1").Uri, testingModelFixture);
      traceContext.TestConsistency(0);
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
        if (!nodesDictionary.ContainsKey(item.UANode.BrowseName.Name))
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
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext(@"ProblemsToReport\fgolra177\Opc.Ua.Semi.NodeSet2.xml");
      traceContext.TestConsistency(0);
      traceContext.Clear();
      traceContext.ValidateAndExportModel(new UriBuilder("https://agileo-automation.com/UA/Semi/").Uri);
      traceContext.TestConsistency(0);
    }

    [TestMethod]
    public void HasOrderedComponentTest()
    {
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext(@"ProblemsToReport\HasOrderedComponent\Opc.Ua.NodeSet2.TriCycleType_V1.1.xml");
      traceContext.TestConsistency(0);
      traceContext.Clear();
      traceContext.ValidateAndExportModel(new UriBuilder("http://tricycleTypeV1").Uri);
      traceContext.TestConsistency(0);
    }

    //[TestMethod]
    public void MachineVisionTest()
    {
      TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext(@"ProblemsToReport\MachineVision\Opc.Ua.MachineVision.NodeSet2.xml");
      traceContext.TestConsistency(0);
      traceContext.Clear();
      traceContext.ValidateAndExportModel(new UriBuilder("http://opcfoundation.org/UA/MachineVision").Uri);
      traceContext.TestConsistency(2);
      Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, traceContext[0].BuildError.Identifier);
      Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, traceContext[1].BuildError.Identifier);
    }
  }
}