//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.BuildingErrorsHandling;

namespace UAOOI.SemanticData.UANodeSetValidation.DataSerialization
{
  [TestClass]
  public class ExtensionsUnitTest
  {
    [TestMethod]
    public void ParseBrowseNameTest()
    {
      Assert.ThrowsException<ArgumentNullException>(() => "".ParseBrowseName(null, x => Assert.Fail()));
      Assert.ThrowsException<ArgumentNullException>(() => "".ParseBrowseName(NodeId.Null, x => Assert.Fail()));
      List<TraceMessage> traceLog = new List<TraceMessage>();
      QualifiedName name =  "".ParseBrowseName(new NodeId("i=105"), x => traceLog.Add(x));
      Assert.IsNotNull(name);
      Assert.IsFalse(String.IsNullOrEmpty(name.Name));
      Assert.IsFalse(name.NamespaceIndexSpecified);
      Assert.AreEqual<int>(1, traceLog.Count);
      Assert.AreEqual<Focus>(Focus.NodeClass, traceLog[0].BuildError.Focus);
      traceLog.Clear();
      name = "".ParseBrowseName(new NodeId("ns=1;i=28"), x => traceLog.Add(x));
      Assert.IsNotNull(name);
      Assert.IsFalse(String.IsNullOrEmpty(name.Name));
      Assert.IsTrue(name.NamespaceIndexSpecified);
      Assert.AreEqual<int>(1, traceLog.Count);
      Assert.AreEqual<Focus>(Focus.NodeClass, traceLog[0].BuildError.Focus);
      Assert.AreEqual<ushort>(1, name.NamespaceIndex);
    }
  }
}
