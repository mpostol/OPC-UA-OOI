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
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UANodeSetValidation.Helpers;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
{
  [TestClass]
  public class ExtensionsUnitTest
  {
    [TestMethod]
    public void ParseBrowseNameNodeIdNullTest()
    {
      Assert.ThrowsException<ArgumentNullException>(() => "".ParseBrowseName(null, x => Assert.Fail()));
      Assert.ThrowsException<ArgumentNullException>(() => "".ParseBrowseName(NodeId.Null, x => Assert.Fail()));
    }

    [TestMethod]
    public void ParseBrowseNameBrowseNameEmptyTest()
    {
      List<TraceMessage> traceLog = new List<TraceMessage>();
      QualifiedName name = "".ParseBrowseName(new NodeId("i=105"), x => traceLog.Add(x));
      Assert.IsNotNull(name);
      Assert.IsFalse(String.IsNullOrEmpty(name.Name));
      Assert.IsTrue(name.Name.StartsWith("EmptyBrowseName_"));
      Assert.IsTrue(name.NamespaceIndexSpecified);
      Assert.AreEqual<int>(1, traceLog.Count);
      Debug.WriteLine(traceLog[0].ToString());
      Assert.AreEqual<Focus>(Focus.NodeClass, traceLog[0].BuildError.Focus);
      Assert.AreEqual<String>(BuildError.EmptyBrowseName.Identifier, traceLog[0].BuildError.Identifier);
    }

    [TestMethod]
    public void ParseBrowseNameBrowseNameSyntaxErrorTest()
    {
      List<TraceMessage> traceLog = new List<TraceMessage>();
      QualifiedName name = "@^#^#^".ParseBrowseName(new NodeId("i=105"), x => traceLog.Add(x));
      Assert.IsNotNull(name);
      Assert.IsFalse(String.IsNullOrEmpty(name.Name));
      Assert.IsTrue(name.Name.StartsWith("EmptyBrowseName_"));
      Assert.IsTrue(name.NamespaceIndexSpecified);
      Assert.AreEqual<int>(1, traceLog.Count);
      Debug.WriteLine(traceLog[0].ToString());
      Assert.AreEqual<Focus>(Focus.DataEncoding, traceLog[0].BuildError.Focus);
      Assert.AreEqual<String>(BuildError.QualifiedNameInvalidSyntax.Identifier, traceLog[0].BuildError.Identifier);
    }

    [TestMethod]
    public void ParseBrowseNameMyNamespaceIndex0TestMethod()
    {
      List<TraceMessage> traceLog = new List<TraceMessage>();
      QualifiedName name = "Id".ParseBrowseName(new NodeId("ns=1;i=28"), x => traceLog.Add(x));
      Assert.IsNotNull(name);
      Assert.IsFalse(String.IsNullOrEmpty(name.Name));
      Assert.AreEqual<string>("Id", name.Name);
      Assert.IsTrue(name.NamespaceIndexSpecified);
      Assert.AreEqual<ushort>(0, name.NamespaceIndex);
      Assert.AreEqual<int>(0, traceLog.Count);
    }

    [TestMethod]
    public void ParseBrowseNameTest()
    {
      List<TraceMessage> traceLog = new List<TraceMessage>();
      QualifiedName name = "   123:Id".ParseBrowseName(new NodeId("ns=1;i=28"), x => traceLog.Add(x));
      Assert.IsNotNull(name);
      Assert.IsFalse(String.IsNullOrEmpty(name.Name));
      Assert.AreEqual<string>("Id", name.Name);
      Assert.IsTrue(name.NamespaceIndexSpecified);
      Assert.AreEqual<ushort>(123, name.NamespaceIndex);
      Assert.AreEqual<int>(0, traceLog.Count);
    }

    [TestMethod]
    public void ParseNodeIdValidTest()
    {
      //Numeric
      NodeId _ni = "i=13".ParseNodeId(x => Assert.Fail());
      Assert.IsNotNull(_ni);
      _ni = "i=0".ParseNodeId(x => Assert.Fail());
      Assert.IsNotNull(_ni);
      //STRING
      _ni = "ns=10;s=Hello:World".ParseNodeId(x => Assert.Fail());
      Assert.IsNotNull(_ni);
      //GUID
      _ni = "g=09087e75-8e5e-499b-954f-f2a9603db28a".ParseNodeId(x => Assert.Fail());
      Assert.IsNotNull(_ni);
      //OPAQUE
      _ni = "ns=1;b=M/RbKBsRVkePCePcx24oRA==".ParseNodeId(x => Assert.Fail());
      Assert.IsNotNull(_ni);
      //?? Default string - should be not valid ?
      _ni = "M/RbKBsRVkePCePcx24oRA==".ParseNodeId(x => Assert.Fail());
      Assert.IsNotNull(_ni);
    }

    [TestMethod]
    public void NodeIdNonValidNumericTest1()
    {
      TraceDiagnosticFixture inMemoryTrace = new Helpers.TraceDiagnosticFixture();
      NodeId _ni = "ns=10;i=-1".ParseNodeId(inMemoryTrace.TraceDiagnostic); //this example is in the specification as valid
      Assert.AreEqual<int>(1, inMemoryTrace.DiagnosticCounter);
      Assert.AreEqual<int>(1, inMemoryTrace.TraceList.Count);
      Assert.AreEqual<string>(BuildError.NodeIdInvalidSyntax.Identifier, inMemoryTrace.TraceList[0].BuildError.Identifier);
    }

    [TestMethod]
    public void NodeIdNonValidNumericTest2()
    {
      TraceDiagnosticFixture inMemoryTrace = new Helpers.TraceDiagnosticFixture();
      NodeId _ni = "ns=-10;i=01".ParseNodeId(inMemoryTrace.TraceDiagnostic); //this example is in the specification as valid
      Assert.AreEqual<int>(1, inMemoryTrace.DiagnosticCounter);
      Assert.AreEqual<int>(1, inMemoryTrace.TraceList.Count);
      Assert.AreEqual<string>(BuildError.NodeIdInvalidSyntax.Identifier, inMemoryTrace.TraceList[0].BuildError.Identifier);
    }
  }
}