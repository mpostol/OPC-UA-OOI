//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  [TestClass]
  public class NamespaceTableUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      NamespaceTable _instance = new NamespaceTable();
      IEnumerable<IModelTableEntry> models = _instance.Models;
      Assert.IsNotNull(models);
      List<IModelTableEntry> listOfExportedNamespaceTable = models.ToList<IModelTableEntry>();
      Assert.AreEqual<int>(1, listOfExportedNamespaceTable.Count);
      Assert.AreEqual<Uri>(new Uri("http://opcfoundation.org/UA/"), listOfExportedNamespaceTable[0].ModelUri);
      Assert.IsNull(listOfExportedNamespaceTable[0].RequiredModel);
    }

    [TestMethod]
    public void GetURIatIndexTest()
    {
      NamespaceTable instance = new NamespaceTable();
      Assert.AreEqual<Uri>(new Uri(Namespaces.OpcUa), instance.GetModelTableEntry(0).ModelUri);
      Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.GetModelTableEntry(1));
      Assert.AreEqual(1, ((INamespaceTable)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
      Assert.AreEqual(2, ((INamespaceTable)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual<Uri>(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2"), instance.GetModelTableEntry(2).ModelUri);
      Assert.AreEqual<Uri>(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1"), instance.GetModelTableEntry(1).ModelUri);
    }

    [TestMethod]
    public void GetURIIndexIndexTest()
    {
      NamespaceTable instance = new NamespaceTable();
      Assert.AreEqual<int>(0, instance.GetURIIndex(new Uri(Namespaces.OpcUa)));
      Assert.AreEqual<int>(-1, instance.GetURIIndex(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1/NonExistingNamespace")));
      Assert.AreEqual(1, ((INamespaceTable)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
      Assert.AreEqual(2, ((INamespaceTable)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual(1, ((INamespaceTable)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
      Assert.AreEqual(2, ((INamespaceTable)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual(2, ((INamespaceTable)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual<int>(2, instance.GetURIIndex(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual<int>(1, instance.GetURIIndex(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
    }

    [TestMethod]
    public void UpadateModelOrAppendTest()
    {
      NamespaceTable instance = new NamespaceTable();
      IModelTableEntry model1 = ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1");
      ((INamespaceTable)instance).RegisterModel(model1);
      IModelTableEntry model2 = ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1");
      ((INamespaceTable)instance).RegisterModel(model2);
      IModelTableEntry model3 = instance.GetModelTableEntry(1);
      Assert.IsNotNull(model3);
      Assert.AreSame(model2, model3);
      Assert.AreNotSame(model1, model3);
    }

    [TestMethod]
    public void ModelsTest()
    {
      NamespaceTable instance = new NamespaceTable();
      ((INamespaceTable)instance).RegisterModel(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1"));
      ((INamespaceTable)instance).RegisterModel(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest3"));
      ((INamespaceTable)instance).RegisterModel(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest2"));
      ((INamespaceTable)instance).RegisterModel(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1"));
      Assert.AreEqual<int>(4, instance.Models.Count<IModelTableEntry>());
    }

    #region instrumentation

    #endregion instrumentation
  }
}