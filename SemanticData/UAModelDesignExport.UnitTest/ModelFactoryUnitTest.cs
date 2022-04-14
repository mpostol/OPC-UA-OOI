//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;
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
      ((IModelFactory)_instance).CreateNamespace(new Uri("NameSpace1", UriKind.Relative), DateTime.UtcNow, null);
      ((IModelFactory)_instance).CreateNamespace(new Uri("NameSpace2", UriKind.Relative), DateTime.UtcNow, null);

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

    [TestMethod]
    public void NamespacesVersionTest()
    {
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      ModelFactory _instance = new ModelFactory(_message => _traceBuffer.Add(_message));
      ((IModelFactory)_instance).CreateNamespace(new Uri("NameSpace1", UriKind.Relative), DateTime.UtcNow, new Version(1, 0, 0));
      ((IModelFactory)_instance).CreateNamespace(new Uri("NameSpace2", UriKind.Relative), DateTime.UtcNow, new Version(1, 0, 0));
      ModelDesign model = _instance.Export();
      Assert.AreEqual<int>(2, model.Namespaces.Length);
      Assert.AreEqual<int>(0, _traceBuffer.Count);
      Assert.AreEqual<string>("1.0.0", model.Namespaces[0].Version);
      Assert.AreEqual<string>("1.0.0", model.Namespaces[1].Version);
    }

    [TestMethod]
    public void ChildrenGeneration()
    {
      List<TraceMessage> _traceBuffer = new List<TraceMessage>();
      ModelFactory _instance = new ModelFactory(_message => _traceBuffer.Add(_message));
      ((IModelFactory)_instance).CreateNamespace(new Uri("NameSpace1", UriKind.Relative), DateTime.UtcNow, new Version(1, 0, 0));
      ((IModelFactory)_instance).CreateNamespace(new Uri("NameSpace2", UriKind.Relative), DateTime.UtcNow, new Version(1, 0, 0));

      IObjectInstanceFactory objectInstanceFactory = ((IModelFactory)_instance).AddNodeFactory<IObjectInstanceFactory>();
      objectInstanceFactory.SymbolicName = new XmlQualifiedName("objectInstanceFactory", "http://a.b.c");
      IVariableInstanceFactory variableInstanceFactory = objectInstanceFactory.AddNodeFactory<IVariableInstanceFactory>();
      variableInstanceFactory.SymbolicName = new XmlQualifiedName("variableInstanceFactory", "http://a.b.c");
      IObjectTypeFactory objectTypeFactory = ((IModelFactory)_instance).AddNodeFactory<IObjectTypeFactory>();
      objectTypeFactory.SymbolicName = new XmlQualifiedName("objectTypeFactory", "http://a.b.c");

      ModelDesign model = _instance.Export();
      Assert.AreEqual<int>(2, model.Items.Length);
      Assert.AreEqual<int>(2, model.Namespaces.Length);
      Assert.AreEqual<int>(0, _traceBuffer.Count);
    }
  }
}