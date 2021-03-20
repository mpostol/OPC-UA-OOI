//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class UAReferenceContextTestClass
  {
    [TestMethod]
    public void NullArgumentConstructorTest()
    {
      Mock<IAddressSpaceBuildContext> asMock = new Mock<IAddressSpaceBuildContext>();
      Mock<IUANodeContext> nodeMock = new Mock<IUANodeContext>();
      Assert.ThrowsException<ArgumentNullException>(() => new UAReferenceContext(null, asMock.Object, nodeMock.Object));
      Assert.ThrowsException<ArgumentNullException>(() => new UAReferenceContext(new XML.Reference(), null, nodeMock.Object));
      Assert.ThrowsException<ArgumentNullException>(() => new UAReferenceContext(new XML.Reference(), asMock.Object, null));
    }
    [TestMethod]
    public void ConstructorTest()
    {
      XML.Reference reference = new XML.Reference() { IsForward = true, ReferenceType = ReferenceTypeIds.HasOrderedComponent.ToString(), Value = "ns=1;i=11" };
      reference.RecalculateNodeIds(x => NodeId.Parse(x));

      Mock<IUANodeContext> typeMock = new Mock<IUANodeContext>();
      typeMock.Setup(x => x.NodeIdContext).Returns(new NodeId("i=10"));

      Mock<IUANodeContext> targetMock = new Mock<IUANodeContext>();
      targetMock.Setup(x => x.NodeIdContext).Returns(new NodeId("ns=1;i=12"));

      Mock<IAddressSpaceBuildContext> asMock = new Mock<IAddressSpaceBuildContext>();
      asMock.Setup(x => x.GetOrCreateNodeContext(It.IsAny<NodeId>(), It.IsAny<Func<NodeId, IUANodeContext>>())).Returns(typeMock.Object);
      asMock.Setup(x => x.GetOrCreateNodeContext(It.Is<NodeId>(z => z == reference.ValueNodeId), It.IsAny<Func<NodeId, IUANodeContext>>())).Returns(targetMock.Object);

      Mock<IUANodeContext> sourceMock = new Mock<IUANodeContext>();
      sourceMock.Setup(x => x.NodeIdContext).Returns(NodeId.Parse("ns=1;i=1"));

      UAReferenceContext instance2Test = new UAReferenceContext(reference, asMock.Object, sourceMock.Object);
      Assert.IsTrue(instance2Test.IsForward);
      Assert.AreEqual<string>("ns=1;i=1:i=10:ns=1;i=12", instance2Test.Key);
      Assert.AreSame(typeMock.Object, instance2Test.TypeNode);
      Assert.AreSame(sourceMock.Object, instance2Test.ParentNode);

      Assert.AreSame(sourceMock.Object, instance2Test.SourceNode);
      Assert.AreSame(targetMock.Object, instance2Test.TargetNode);

      reference.IsForward = false;
      instance2Test = new UAReferenceContext(reference, asMock.Object, sourceMock.Object);
      Assert.IsFalse(instance2Test.IsForward);
      Assert.AreEqual<string>("ns=1;i=12:i=10:ns=1;i=1", instance2Test.Key);
      Assert.AreSame(typeMock.Object, instance2Test.TypeNode);
      Assert.AreSame(sourceMock.Object, instance2Test.ParentNode);

      Assert.AreSame(sourceMock.Object, instance2Test.TargetNode);
      Assert.AreSame(targetMock.Object, instance2Test.SourceNode);
    }
  }
}