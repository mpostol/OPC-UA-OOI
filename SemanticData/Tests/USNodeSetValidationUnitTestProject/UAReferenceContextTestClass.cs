//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Xml;
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
      asMock.Verify(z => z.GetOrCreateNodeContext(It.IsAny<NodeId>(), It.IsAny<Func<NodeId, IUANodeContext>>()), Times.Exactly(2));

      reference.IsForward = false;
      instance2Test = new UAReferenceContext(reference, asMock.Object, sourceMock.Object);
      Assert.IsFalse(instance2Test.IsForward);
      Assert.AreEqual<string>("ns=1;i=12:i=10:ns=1;i=1", instance2Test.Key);
      Assert.AreSame(typeMock.Object, instance2Test.TypeNode);
      Assert.AreSame(sourceMock.Object, instance2Test.ParentNode);
      Assert.AreSame(sourceMock.Object, instance2Test.TargetNode);
      Assert.AreSame(targetMock.Object, instance2Test.SourceNode);
    }

    [TestMethod]
    public void GetReferenceTypeNameTest()
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

      asMock.Setup(x => x.ExportBrowseName(It.Is<NodeId>(z => z == new NodeId("i=10")), It.IsAny<NodeId>())).Returns(new XmlQualifiedName("2P8ZkTA2Ccahvs", "943IbIVI6ivpBj"));
      XmlQualifiedName typeName = instance2Test.GetReferenceTypeName();
      asMock.Verify(x => x.ExportBrowseName(It.Is<NodeId>(z => z == new NodeId("i=10")), It.IsAny<NodeId>()), Times.Once);
      Assert.IsNotNull(typeName);
      Assert.AreEqual<string>("943IbIVI6ivpBj", typeName.Namespace);
      Assert.AreEqual<string>("2P8ZkTA2Ccahvs", typeName.Name);
    }

    [TestMethod]
    public void BrowsePathNameIsForwardTest()
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

      targetMock.Setup(z => z.BuildSymbolicId(It.Is<List<string>>(x => CreatePathFixture(x))));
      XmlQualifiedName typeName = instance2Test.BrowsePath();
      targetMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Once);
      sourceMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Never);
      typeMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Never);
      Assert.IsNotNull(typeName);
      Assert.AreEqual<string>("", typeName.Namespace);
      Assert.AreEqual<string>("2P8ZkTA2Ccahvs_bLAsL6DSp1Ow5d", typeName.Name);
    }

    [TestMethod]
    public void BrowsePathNameTest()
    {
      XML.Reference reference = new XML.Reference() { IsForward = false, ReferenceType = ReferenceTypeIds.HasOrderedComponent.ToString(), Value = "ns=1;i=11" };
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

      Assert.IsFalse(instance2Test.IsForward);
      targetMock.Setup(z => z.BuildSymbolicId(It.Is<List<string>>(x => CreatePathFixture(x))));
      XmlQualifiedName typeName = instance2Test.BrowsePath();
      targetMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Once);
      sourceMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Never);
      typeMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Never);
      Assert.IsNotNull(typeName);
      Assert.AreEqual<string>("", typeName.Namespace);
      Assert.AreEqual<string>("2P8ZkTA2Ccahvs_bLAsL6DSp1Ow5d", typeName.Name);
    }


    [TestMethod]
    public void BuildSymbolicIdTest()
    {
      XML.Reference reference = new XML.Reference() { IsForward = false, ReferenceType = ReferenceTypeIds.HasOrderedComponent.ToString(), Value = "ns=1;i=11" };
      reference.RecalculateNodeIds(x => NodeId.Parse(x));

      Mock<IUANodeContext> typeMock = new Mock<IUANodeContext>();

      Mock<IUANodeContext> targetMock = new Mock<IUANodeContext>();
      targetMock.Setup(x => x.NodeIdContext).Returns(new NodeId("ns=1;i=12"));

      Mock<IAddressSpaceBuildContext> asMock = new Mock<IAddressSpaceBuildContext>();
      asMock.Setup(x => x.GetOrCreateNodeContext(It.IsAny<NodeId>(), It.IsAny<Func<NodeId, IUANodeContext>>())).Returns(typeMock.Object);
      asMock.Setup(x => x.GetOrCreateNodeContext(It.Is<NodeId>(z => z == reference.ValueNodeId), It.IsAny<Func<NodeId, IUANodeContext>>())).Returns(targetMock.Object);

      Mock<IUANodeContext> sourceMock = new Mock<IUANodeContext>();
      sourceMock.Setup(x => x.NodeIdContext).Returns(NodeId.Parse("ns=1;i=1"));

      UAReferenceContext instance2Test = new UAReferenceContext(reference, asMock.Object, sourceMock.Object);

      Assert.IsFalse(instance2Test.IsForward);
      targetMock.Setup(z => z.BuildSymbolicId(It.Is<List<string>>(x => CreatePathFixture(x))));
      List<string> path = new List<string>();
      instance2Test.BuildSymbolicId(path);
      targetMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Once);
      sourceMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Never);
      typeMock.Verify(z => z.BuildSymbolicId(It.IsAny<List<string>>()), Times.Never);
      Assert.AreEqual<int>(2, path.Count);
    }

    private bool CreatePathFixture(List<string> z)
    {
      z.Add("2P8ZkTA2Ccahvs");
      z.Add("bLAsL6DSp1Ow5d");
      return z.Count == 2;
    }
  }
}