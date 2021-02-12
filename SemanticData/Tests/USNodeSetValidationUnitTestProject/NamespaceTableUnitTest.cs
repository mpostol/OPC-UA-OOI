//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;
using UAOOI.SemanticData.UANodeSetValidation.Utilities;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  //TODO NamespaceUrisCannotBeNull - is reported for UADefinedTypes #520
  [TestClass]
  public class NamespaceTableUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      NamespaceTable _instance = new NamespaceTable();
      IEnumerable<IModelTableEntry> exportedNamespaceTable = _instance.ExportNamespaceTable;
      Assert.IsNotNull(exportedNamespaceTable);
      List<IModelTableEntry> listOfExportedNamespaceTable = exportedNamespaceTable.ToList<IModelTableEntry>();
      Assert.AreEqual<int>(1, listOfExportedNamespaceTable.Count);
      listOfExportedNamespaceTable[0].
    }

    [TestMethod]
    public void GetStringTest()
    {
      NamespaceTable _instance = new NamespaceTable();
      Assert.AreEqual<string>(Namespaces.OpcUa, _instance.GetString(0));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetStringArgumentOutOfRangeExceptionTest()
    {
      NamespaceTable _instance = new NamespaceTable();
      string _uri = _instance.GetString(1);  //It should throw ArgumentOutOfRangeException exception
    }

    [TestMethod]
    public void GetIndexTest()
    {
      NamespaceTable _instance = new NamespaceTable();
      Assert.AreEqual<int>(0, _instance.GetIndex().First<ushort>());
      Assert.AreEqual<int>(0, _instance.GetIndex(Namespaces.OpcUa));
      Assert.AreEqual<int>(-1, _instance.GetIndex("non existing namespace"));
    }

    [TestMethod]
    public void GetIndexOrAppend()
    {
      NamespaceTable _instance = new NamespaceTable();
      Assert.AreEqual<int>(1, _instance.GetIndexOrAppend("qerqrqerqwrewrwer"));
      Assert.AreEqual<int>(1, _instance.GetIndex("qerqrqerqwrewrwer"));
      Assert.AreEqual<int>(1, _instance.GetIndex().First<ushort>());
    }
  }
}