//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  [TestClass]
  public class NamespaceTableUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {

      List<TraceMessage> _log = new List<TraceMessage>();
      NamespaceTable _instance = new NamespaceTable(_message => _log.Add(_message));
      Assert.AreEqual<int>(1, _instance.Count);
      Assert.AreEqual<int>(0, _log.Count);

    }
    [TestMethod]
    public void GetStringTest()
    {

      List<TraceMessage> _log = new List<TraceMessage>();
      NamespaceTable _instance = new NamespaceTable(_message => _log.Add(_message));
      Assert.AreEqual<string>(Namespaces.OpcUa, _instance.GetString(0));
      Assert.IsNull(_instance.GetString(1));  //It should throw na exception
      Assert.AreEqual<int>(0, _log.Count); //Trace doesn't work

    }
    [TestMethod]
    public void GetIndexTest()
    {

      List<TraceMessage> _log = new List<TraceMessage>();
      NamespaceTable _instance = new NamespaceTable(_message => _log.Add(_message));
      Assert.AreEqual<int>(0, _instance.GetIndex(Namespaces.OpcUa));
      Assert.AreEqual<int>(-1, _instance.GetIndex("qerqrqerqwrewrwer"));
      Assert.AreEqual<int>(0, _log.Count);

    }
    [TestMethod]
    public void GetIndexOrAppend()
    {

      List<TraceMessage> _log = new List<TraceMessage>();
      NamespaceTable _instance = new NamespaceTable(_message => _log.Add(_message));
      Assert.AreEqual<int>(1, _instance.GetIndexOrAppend("qerqrqerqwrewrwer", _message => _log.Add(_message)));
      Assert.AreEqual<int>(1, _instance.GetIndex("qerqrqerqwrewrwer"));
      Assert.AreEqual<int>(0, _log.Count); //Trace doesn't work

    }
  }
}
