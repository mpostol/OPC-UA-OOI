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
      Assert.AreEqual<Uri>(new Uri(Namespaces.OpcUa), instance.GetURIatIndex(0).ModelUri);
      Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.GetURIatIndex(1));
      Assert.AreEqual(1, ((IAddressSpaceURIRecalculate)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
      Assert.AreEqual(2, ((IAddressSpaceURIRecalculate)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual<Uri>(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2"), instance.GetURIatIndex(2).ModelUri);
      Assert.AreEqual<Uri>(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1"), instance.GetURIatIndex(1).ModelUri);
    }

    [TestMethod]
    public void GetURIIndexIndexTest()
    {
      NamespaceTable instance = new NamespaceTable();
      Assert.AreEqual<int>(0, instance.GetURIIndex(new Uri(Namespaces.OpcUa)));
      Assert.AreEqual<int>(-1, instance.GetURIIndex(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1/NonExistingNamespace")));
      Assert.AreEqual(1, ((IAddressSpaceURIRecalculate)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
      Assert.AreEqual(2, ((IAddressSpaceURIRecalculate)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual(1, ((IAddressSpaceURIRecalculate)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
      Assert.AreEqual(2, ((IAddressSpaceURIRecalculate)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual(2, ((IAddressSpaceURIRecalculate)instance).GetURIIndexOrAppend(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual<int>(2, instance.GetURIIndex(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest2")));
      Assert.AreEqual<int>(1, instance.GetURIIndex(new Uri("http://opcfoundation.org/UA/GetURIatIndexTest1")));
    }

    [TestMethod]
    public void UpadateModelOrAppendTest()
    {
      NamespaceTable instance = new NamespaceTable();
      IModelTableEntry model1 = ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1");
      ((IAddressSpaceURIRecalculate)instance).UpadateModelOrAppend(model1);
      IModelTableEntry model2 = ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1");
      ((IAddressSpaceURIRecalculate)instance).UpadateModelOrAppend(model2);
      IModelTableEntry model3 = instance.GetURIatIndex(1);
      Assert.IsNotNull(model3);
      Assert.AreSame(model2, model3);
      Assert.AreNotSame(model1, model3);
    }

    [TestMethod]
    public void ModelsTest()
    {
      NamespaceTable instance = new NamespaceTable();
      ((IAddressSpaceURIRecalculate)instance).UpadateModelOrAppend(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1"));
      ((IAddressSpaceURIRecalculate)instance).UpadateModelOrAppend(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest3"));
      ((IAddressSpaceURIRecalculate)instance).UpadateModelOrAppend(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest2"));
      ((IAddressSpaceURIRecalculate)instance).UpadateModelOrAppend(ModelTableEntry.GetDefaultModelTableEntry("http://opcfoundation.org/UA/GetURIatIndexTest1"));
      Assert.AreEqual<int>(4, instance.Models.Count<IModelTableEntry>());
    }

    #region instrumentation

    private class ModelTableEntry : IModelTableEntry
    {
      /// <summary>
      /// Gets the default model table entry.
      /// </summary>
      /// <param name="modelUri">The model URI.</param>
      /// <param name="index">Index of the model.</param>
      /// <returns>UAOOI.SemanticData.UANodeSetValidation.Utilities.IModelTableEntry.</returns>
      /// <remarks>This type is defined in Part 6 F.5 but the definition is not compliant with the UANodeSet schema.
      /// This type is also defined in the Part 3 5.2.9 but the definition is not compliant.</remarks>
      internal static ModelTableEntry GetDefaultModelTableEntry(string modelUri)
      {
        return new ModelTableEntry
        {
          AccessRestrictions = 0xC,
          ModelUri = new Uri(modelUri),
          PublicationDate = DateTime.FromFileTime(999999),
          RequiredModel = null,
          RolePermissions = new IRolePermission[] { },
          Version = new Version(1, 0).ToString()
        };
      }

      #region IModelTableEntry

      /// <summary>
      /// Gets or sets the role permissions. The list of default RolePermissions for all Nodes in the model.
      /// </summary>
      /// <value>The role permissions.</value>
      public IRolePermission[] RolePermissions { get; set; }

      /// <summary>
      /// Gets or sets the required model. A list of dependencies for the model. If the model requires a minimum version the PublicationDate shall be specified.
      /// Tools which attempt to resolve these dependencies may accept any PublicationDate after this date.
      /// </summary>
      /// <value>The required model.</value>
      public IModelTableEntry[] RequiredModel { get; set; }

      /// <summary>
      /// Gets or sets the model URI. The URI for the model. This URI should be one of the entries in the <see cref="NamespaceTable"/> table.
      /// </summary>
      /// <value>The model URI.</value>
      public Uri ModelUri { get; set; }

      /// <summary>
      /// Gets or sets the version. The version of the model defined in the UANodeSet. This is a human readable string and not intended for programmatic comparisons.
      /// </summary>
      /// <value>The version.</value>
      public string Version { get; set; }

      /// <summary>
      /// Gets or sets the publication date. When the model was published. This value is used for comparisons if the model is defined in multiple UANodeSet files.
      /// </summary>
      /// <value>The publication date.</value>
      public DateTime? PublicationDate { get; set; }

      /// <summary>
      /// Gets or sets the access restrictions. The default AccessRestrictions that apply to all Nodes in the model.
      /// </summary>
      /// <value>The access restrictions.</value>
      public byte AccessRestrictions { get; set; }

      #endregion IModelTableEntry
    }

    #endregion instrumentation
  }
}