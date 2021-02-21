//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

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
    [TestMethod]
    public void ADITest()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\ADI#509\Opc.Ua.Adi.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext addressSpace = traceContext.CreateAddressSpaceContext();
        Uri model = addressSpace.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(1, traceContext.TraceList.Count);
        Assert.AreEqual<string>(BuildError.ModelsCannotBeNull.Identifier, traceContext.TraceList[0].BuildError.Identifier);
        traceContext.Clear();
        addressSpace.ValidateAndExportModel(model);
        //TODO ADI model from Embedded example import fails #509
        Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.DataEncoding).Count<TraceMessage>());
        Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.DataType).Count<TraceMessage>());
        Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Naming).Count<TraceMessage>());
        Assert.AreEqual<int>(3, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NodeClass).Count<TraceMessage>());
        Assert.AreEqual<int>(0, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.NonCategorized).Count<TraceMessage>());
        Assert.AreEqual<int>(23, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.Reference).Count<TraceMessage>());
        Assert.AreEqual<int>(1, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Focus == Focus.XML).Count<TraceMessage>());
        //errors
        Assert.AreEqual<int>(27, traceContext.TraceList.Count);
        Assert.AreEqual<int>(3, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.NodeIdNotDefined.Identifier).Count<TraceMessage>());
        Assert.AreEqual<int>(5, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.UndefinedHasSubtypeTarget.Identifier).Count<TraceMessage>());
        Assert.AreEqual<int>(18, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.UndefinedHasTypeDefinition.Identifier).Count<TraceMessage>());
        Assert.AreEqual<int>(1, traceContext.TraceList.Where<TraceMessage>(x => x.BuildError.Identifier == BuildError.ModelContainsErrors.Identifier).Count<TraceMessage>());
        Assert.AreEqual<string>(BuildError.ModelContainsErrors.Identifier, traceContext.TraceList[26].BuildError.Identifier);
      }
    }

    [TestMethod]
    public void eoursel510Test()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\eoursel510\Opc.Ua.NodeSet2.TriCycleType_V1.1.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext addressSpace = traceContext.CreateAddressSpaceContext();
        ModelFactoryTestingFixture.InformationModelFactoryBase testingModelFixture = new InformationModelFactoryBase();
        addressSpace.InformationModelFactory = testingModelFixture;
        Uri model = addressSpace.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(0, traceContext.TraceList.Count);
        traceContext.Clear();
        addressSpace.ValidateAndExportModel(model);
        Assert.AreEqual<int>(5, traceContext.TraceList.Count);
        IEnumerable<NodeFactoryBase> nodes = testingModelFixture.Export();
        Assert.AreEqual(21, nodes.Count< NodeFactoryBase>());
        Dictionary<string, NodeFactoryBase> nodesDictionary = nodes.ToDictionary<NodeFactoryBase, string>(x => x.SymbolicName.Name);
        AddressSpaceContext asContext = addressSpace as AddressSpaceContext;
        //TODO Add a warning that the AS contains nodes orphaned and inaccessible for browsing starting from the Root node #529
        IEnumerable<IUANodeContext> allNodes = null;
        asContext.UTValidateAndExportModel(1, x => allNodes = x);
        Assert.IsNotNull(allNodes);
        List<IUANodeContext> orphanedNodes = new List<IUANodeContext>();
        foreach (IUANodeContext item in allNodes)
        {
          if (!nodesDictionary.ContainsKey(item.BrowseName.ToString()))
          {
            orphanedNodes.Add(item);
            Debug.WriteLine($"The following node has been removed from the model: {item.ToString()}");
          }
        }
        Debug.WriteLine($"The recovered information model contains {nodes.Count<NodeFactoryBase>()} nodes");
        Debug.WriteLine($"The source information model contains {allNodes.Count<IUANodeContext>()} nodes");
        Debug.WriteLine($"Number of nodes not considered for export {orphanedNodes.Count<IUANodeContext>()}");
      }
    }

    [TestMethod]
    public void fgolra177Test()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\fgolra177\Opc.Ua.Semi.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext addressSpace = traceContext.CreateAddressSpaceContext();
        Uri model = addressSpace.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(0, traceContext.TraceList.Count);
        traceContext.Clear();
        addressSpace.ValidateAndExportModel(model);
        Assert.AreEqual<int>(0, traceContext.TraceList.Count);
      }
    }

    [TestMethod]
    public void MachineVisionTest()
    {
      FileInfo _testDataFileInfo = new FileInfo(@"ProblemsToReport\MachineVision\Opc.Ua.MachineVision.NodeSet2.xml");
      Assert.IsTrue(_testDataFileInfo.Exists);
      using (TracedAddressSpaceContext traceContext = new TracedAddressSpaceContext())
      {
        IAddressSpaceContext addressSpace = traceContext.CreateAddressSpaceContext();
        Uri model = addressSpace.ImportUANodeSet(_testDataFileInfo);
        Assert.AreEqual<int>(0, traceContext.TraceList.Count);
        traceContext.Clear();
        addressSpace.ValidateAndExportModel(model);
        Assert.AreEqual<int>(3, traceContext.TraceList.Count);
        Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, traceContext.TraceList[0].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.WrongInverseName.Identifier, traceContext.TraceList[1].BuildError.Identifier);
        Assert.AreEqual<string>(BuildError.ModelContainsErrors.Identifier, traceContext.TraceList[2].BuildError.Identifier);
      }
    }
  }
}