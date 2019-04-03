//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Xml;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  [TestClass]
  public class DataTypeDefinitionFactoryBaseUnitTest
  {

    [TestMethod]
    public void ConstructorTest()
    {
      DataTypeDefinitionFactoryBase _newInstance = new DataTypeDefinitionFactoryBase(x => { })
      {
        IsOptionSet = true,
        IsUnion = true, //Is not supported by the UA Model Design
        Name = new XmlQualifiedName("XmlQualifiedName"), // Is not supported by the UA Model Design
        SymbolicName = "SymbolicName" // Is not supported by the UA Model Design
      };
      XML.DataTypeDesign _dataType = _newInstance.Export();
      Assert.IsNotNull(_dataType);
      Assert.IsNotNull(_dataType.Fields);
      Assert.AreEqual<int>(0, _dataType.Fields.Count());
      Assert.IsNull(_dataType.Encodings);
      Assert.IsTrue(_dataType.IsOptionSet);
      Assert.IsFalse(_dataType.NoArraysAllowed);
    }
    [TestMethod]
    public void NewFieldTest()
    {
      DataTypeDefinitionFactoryBase _newInstance = new DataTypeDefinitionFactoryBase(x => { }) { };
      IDataTypeFieldFactory _newField = _newInstance.NewField();
      XML.DataTypeDesign _dataType = _newInstance.Export();
      Assert.IsNotNull(_dataType);
      Assert.IsNotNull(_dataType.Fields);
      Assert.AreEqual<int>(1, _dataType.Fields.Count());
    }

  }
}
