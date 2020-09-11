//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test
{
  [TestClass]
  public class RepositoryGroupUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      RepositoryGroup _itemToTest = new RepositoryGroup();
      Assert.ThrowsException<NotImplementedException>(() => _itemToTest.Add("Key", null));
      Assert.ThrowsException<NotImplementedException>(() => _itemToTest.Add(new KeyValuePair<string, object>("Key", null)));
      Assert.ThrowsException<NotImplementedException>(() => _itemToTest.Clear());
      Assert.ThrowsException<NotImplementedException>(() => _itemToTest.Remove("Random name"));
      Assert.IsNotNull(_itemToTest.GetEnumerator());
      Assert.IsNotNull(_itemToTest.Keys);
      Assert.AreEqual<int>(0, _itemToTest.Count);
    }

    [TestMethod]
    public void GetConsumerBindingJsonSerializationTest()
    {
      RepositoryGroup _itemToTest = new RepositoryGroup();
      string processValueName = "processValueName";
      Action<String> updater = _itemToTest.AddProperty<string>(processValueName);
      Assert.IsNotNull(updater);
      Assert.AreEqual<int>(1, _itemToTest.Count);
      Assert.AreEqual<string>(default(string), (string)_itemToTest[processValueName]);
      updater("New value");
      Assert.AreEqual<string>("New value", (string)_itemToTest[processValueName]);
      string dto = _itemToTest.ToString();
      Debug.Write(dto);
      Assert.AreEqual<int>(32, dto.Length);
      Assert.AreEqual<string>("{\"processValueName\":\"New value\"}", dto);

    }
  }
}