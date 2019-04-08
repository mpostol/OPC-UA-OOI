//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  public class ModelFactoryUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void EEmptyModelTest()
    {
      ModelFactory _instance = new ModelFactory(_message => { });
      ModelDesign _createdModel = _instance.Export();
    }
    [TestMethod]
    public void NamespaceTest()
    {
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      ModelFactory _instance = new ModelFactory(_message => _traceBuffer.Add(_message));
      ((IModelFactory)_instance).CreateNamespace("NameSpace1", String.Empty, String.Empty);
      ((IModelFactory)_instance).CreateNamespace("NameSpace2", String.Empty, String.Empty);
      ModelDesign _createdModel = _instance.Export();
      Assert.IsNotNull(_createdModel);
      Assert.IsNotNull(_createdModel.Items);
      Assert.IsNull(_createdModel.AnyAttr);
      Assert.AreEqual<int>(0, _createdModel.Items.Length);
      Assert.AreEqual<int>(2, _createdModel.Namespaces.Length);
      Assert.AreEqual<string>("NameSpace2", _createdModel.TargetNamespace);
      Assert.AreEqual<DateTime>(DateTime.Now.Date, _createdModel.TargetPublicationDate.Date);
      Assert.IsTrue(_createdModel.TargetPublicationDateSpecified);
      Assert.AreEqual<int>(0, _traceBuffer.Count);
    }
  }
}
