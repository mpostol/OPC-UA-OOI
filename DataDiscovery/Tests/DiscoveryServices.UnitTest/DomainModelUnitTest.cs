﻿//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UAOOI.DataDiscovery.DiscoveryServices.Models;
using UAOOI.DataDiscovery.DiscoveryServices.UnitTest.TestData;

namespace UAOOI.DataDiscovery.DiscoveryServices.UnitTest
{
  [TestClass]
  public class DomainModelUnitTest
  {
    [TestMethod]
    [DeploymentItem(@"TestData\", @"TestData\")]
    public void DeserializeAutoGeneratedXmlTest()
    {
      FileInfo file = new FileInfo(@"TestData\DomainModel.xml");
      Assert.IsTrue(file.Exists);
      DomainModel newDescription = null;
      using (Stream _descriptionStream = file.OpenRead())
      {
        XmlSerializer _serializer = new XmlSerializer(typeof(DomainModel));
        newDescription = (DomainModel)_serializer.Deserialize(_descriptionStream);
      }
      Assert.IsNotNull(newDescription);
      Assert.IsFalse(string.IsNullOrEmpty(newDescription.AliasName));
      Assert.IsFalse(string.IsNullOrEmpty(newDescription.Description));
      Assert.IsFalse(string.IsNullOrEmpty(newDescription.DomainModelGuidString));
      Assert.IsFalse(string.IsNullOrEmpty(newDescription.DomainModelUriString));
      Assert.IsFalse(string.IsNullOrEmpty(newDescription.UniversalAddressSpaceLocator));
      Assert.IsFalse(string.IsNullOrEmpty(newDescription.UniversalAuthorizationServerLocator));
      Assert.IsTrue(string.IsNullOrEmpty(newDescription.UniversalDiscoveryServiceLocator));
    }

    [TestMethod]
    public void TypeDictionariesTestMethod()
    {
      DomainModel domainModel = ReferenceDomainModel.GerReferenceDomainModel();
      Assert.IsNotNull(domainModel.TypeDictionaries);
      Dictionary<string, TypeDictionaryWitKey> _dictionary = (from x in domainModel.TypeDictionaries
                                                              from y in x.StructuredType
                                                              select new TypeDictionaryWitKey { Key = $"{x.TargetNamespace}:{y.Name}", Dictionary = y }).ToDictionary<TypeDictionaryWitKey, string>(z => z.Key);
      Assert.AreEqual<int>(2, _dictionary.Count);
    }

    [TestMethod]
    public void SerializeTestMethod()
    {
      string fileName = "ReferenceDomainModel.xml";
      DomainModel _dm = ReferenceDomainModel.GerReferenceDomainModel();
      FileInfo file = new FileInfo($@"TestData\{fileName}");
      using (Stream _outputStream = file.Create())
      {
        XmlSerializer _serializer = new XmlSerializer(typeof(DomainModel));
        _serializer.Serialize(_outputStream, _dm);
      }
      Assert.IsTrue(file.Exists);
      Assert.IsTrue(file.Length > 0);
      using (Stream _descriptionStream = file.OpenRead())
      {
        XmlSerializer _serializer = new XmlSerializer(typeof(DomainModel));
        DomainModel newDescription = (DomainModel)_serializer.Deserialize(_descriptionStream);
        Assert.IsNotNull(newDescription);
      }
    }

    [TestMethod]
    public void TestDataFolderContentTestMethod()
    {
      FileInfo _fi = new FileInfo(@"TestData\root.zone\commsvr.com\UA\Examples\BoilersSet\DomainModel.xml");
      Assert.IsTrue(_fi.Exists);
      _fi = new FileInfo(@"TestData\root.zone\commsvr.com\UA\Examples\BoilersSet\DomainDescriptor.xml");
      Assert.IsTrue(_fi.Exists);
      _fi = new FileInfo(@"TestData\root.zone\commsvr.com\UA\Examples\BoilersSet\Commsvr.UA.Examples.BoilersSet.NodeSet2.xml");
      Assert.IsTrue(_fi.Exists);
    }

    private class TypeDictionaryWitKey
    {
      public string Key;
      public StructuredType Dictionary;
    }
  }
}