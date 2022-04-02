//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class ValidatorUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      Mock<IAddressSpaceBuildContext> addressSpaceBuildContextMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IBuildErrorsHandling> buildErrorsHandlingNock = new Mock<IBuildErrorsHandling>();
      Validator _i2t = new Validator(addressSpaceBuildContextMock.Object, buildErrorsHandlingNock.Object);
    }

    [TestMethod]
    public void UAMethodTest()
    {
      //TODO The exported model doesn't contain all nodes #653
      //Assert.Inconclusive("The exported model doesn't contain all nodes #653");

      Mock<IAddressSpaceBuildContext> addressSpaceBuildContextMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IBuildErrorsHandling> buildErrorsHandlingNock = new Mock<IBuildErrorsHandling>();
      Validator _i2t = new Validator(addressSpaceBuildContextMock.Object, buildErrorsHandlingNock.Object);
      UAVariable inputParameter = new UAVariable()
      {
        NodeId = "ns=2;i=1031",
        BrowseName = "InputArguments",
        ParentNodeId = "ns=2;i=7001",
        DataType = "i=296",
        ValueRank = 1,
        ArrayDimensions = "0",
        DisplayName = new LocalizedText[] { new LocalizedText() { Value = "InputArguments" } },
        Description = new LocalizedText[] { new LocalizedText() { Value = "the definition of the input argument of method 1:MethodSet.2:Start" } },

        References = new Reference[]
        {
          new Reference() { ReferenceType= "i=46", IsForward=false, Value = "ns=2;i=7001" }
        }
      };
      UAMethod method = new UAMethod()
      {
        NodeId = "ns=2;i=7001",
        BrowseName = "2:Start",
        ParentNodeId = "ns=2;i=5002",
        DisplayName = new LocalizedText[] { new LocalizedText() { Value = "Start" } },
        References = new Reference[] { }
      };
      List<TraceMessage> traceBuffer = new List<TraceMessage>();
      UANodeContext nodeContext = new UANodeContext(DataSerialization.NodeId.Parse(method.NodeId), addressSpaceBuildContextMock.Object, x => traceBuffer.Add(x));
      nodeContext.Update(method, x => { });
      Mock<INodeContainer> nodeContainerMock = new Mock<INodeContainer>();
      Mock<IMethodInstanceFactory> methodInstanceFactory = new Mock<IMethodInstanceFactory>();
      nodeContainerMock.Setup(x => x.AddNodeFactory<IMethodInstanceFactory>()).Returns(methodInstanceFactory.Object);
      //TODO UANodeSetValidation.Extensions.GetObject - object reference not set #624
      Assert.Inconclusive("UANodeSetValidation.Extensions.GetObject - object reference not set #624");
      _i2t.ValidateExportNode(nodeContext, null, nodeContainerMock.Object,  z => { });
      Assert.AreEqual<int>(0, traceBuffer.Count);
    }
  }
}