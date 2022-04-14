//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UnitTest.Helpers;

namespace UAOOI.SemanticData.UANodeSetValidation.XML
{
  [TestClass]
  public class UANodeUnitTest
  {
    [TestMethod]
    public void ModelTableEntryTest()
    {
      ModelTableEntry item2Test = new ModelTableEntry()
      {
        AccessRestrictions = 0xC,
        ModelUri = "http://www.commsvr.com/",
        PublicationDate = DateTime.UtcNow.Date,
        PublicationDateSpecified = false,
        RequiredModel = null,
        RolePermissions = null,
        Version = "1.04"
      };
      IModelTableEntry itemToCheck = item2Test;
      Assert.AreEqual<byte>(0xC, itemToCheck.AccessRestrictions);
      Assert.IsTrue(itemToCheck.ModelUri == new Uri("http://www.commsvr.com/"));
      Assert.IsFalse(itemToCheck.PublicationDate.HasValue);
      Assert.ThrowsException<InvalidOperationException>(() => itemToCheck.PublicationDate.Value);
      Assert.IsNull(itemToCheck.RequiredModel);
      itemToCheck.Version.CompareTo(new Version(1, 4));
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void UAReferenceTypeEqualsTest()
    {
      UAReferenceType _first = TestData.CreateUAReferenceType();
      UAReferenceType _second = TestData.CreateUAReferenceType();
      Assert.IsTrue(_first == _second);
    }

    [TestMethod]
    public void RecalculateNodeIdsTest()
    {
      Mock<IUAModelContext> modelMock = new Mock<IUAModelContext>();
      modelMock.Setup(x => x.ImportNodeId(It.IsAny<string>(), It.IsAny<Action<TraceMessage>>())).Returns<string, Action<TraceMessage>>((q, w) => NodeId.Parse(q));
      modelMock.Setup(x => x.ImportBrowseName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<TraceMessage>>())).Returns<string, string, Action<TraceMessage>>((a, b, c) => (QualifiedName.Parse(a), NodeId.Parse(b)));
      //modelMock.Setup(x => x.ModelUri);
      UAObject toTest = TestData.CreateUAObject();
      toTest.RecalculateNodeIds(modelMock.Object, XML => Assert.Fail());
      Assert.IsNotNull(toTest.NodeIdNodeId);
      Assert.AreEqual<string>("ns=1;i=1", toTest.NodeIdNodeId.ToString());
      Assert.IsNotNull(toTest.BrowseNameQualifiedName);
      Assert.AreEqual<string>("1:NewUAObject", toTest.BrowseNameQualifiedName.ToString());
      modelMock.Verify(x => x.ImportBrowseName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Action<TraceMessage>>()), Times.Once);
      modelMock.Verify(x => x.ImportNodeId(It.IsAny<string>(), It.IsAny<Action<TraceMessage>>()), Times.Exactly(5));
      //modelMock.Verify(x => x.ModelUri, Times.Never);

      Assert.IsNotNull(toTest.References[0].ReferenceTypeNodeid);
      Assert.IsNotNull(toTest.References[1].ReferenceTypeNodeid);
      Assert.IsNotNull(toTest.References[0].ValueNodeId);
      Assert.IsNotNull(toTest.References[1].ValueNodeId);
      Assert.IsNotNull(toTest.ParentNodeIdNodeId);
    }

    [TestMethod]
    public void UANodeEquals()
    {
      UANode _first = TestData.CreateUAObject().Recalculate();
      UANode _second = TestData.CreateUAObject().Recalculate();
      Assert.AreNotSame(_first, _second); //to make sure we have two objects
      Assert.IsTrue(_first != null);
      Assert.IsTrue(null != _first);
      Assert.IsTrue(_first == _second);
      Assert.IsTrue(_second == _first);
    }

    [TestMethod]
    public void UAInstanceEqualsTest()
    {
      UAInstance _first = TestData.CreateUAObject().Recalculate<UAObject>();
      UAInstance _second = TestData.CreateUAObject().Recalculate<UAObject>();
      _second.ParentNodeId = System.Guid.NewGuid().ToString();
      _second.NodeId = System.Guid.NewGuid().ToString();
      _second.BrowseName = System.Guid.NewGuid().ToString();
      Assert.IsTrue(_first == _second);
    }

    [TestMethod]
    public void UAObjectEqualsTest()
    {
      UAObject _first = TestData.CreateUAObject().Recalculate<UAObject>();
      UAObject _second = TestData.CreateUAObject().Recalculate<UAObject>();
      _second.EventNotifier ^= _second.EventNotifier;
      Assert.IsTrue(_first != _second);
    }
  }
}