using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UAOOI.Networking.DataRepository.AzureGateway.Test
{
  [TestClass]
  public class RepositoryGroupUnitTest
  {
    [TestMethod]
    public void ConstructorTest()
    {
      dynamic _newInstance = new RepositoryGroup();
      Assert.ThrowsException<Microsoft.CSharp.RuntimeBinder.RuntimeBinderException>(() => _newInstance.qwerty);
    }

    [TestMethod]
    public void AddPropertyTest()
    {
      RepositoryGroup _newInstance = new RepositoryGroup();
      Action<string> _updater = _newInstance.AddProperty<string>("qwerty");
      Assert.IsNotNull(_updater);
      dynamic _dynamicIbstance = _newInstance;
      Assert.IsNotNull(_dynamicIbstance);
      Assert.IsTrue(string.IsNullOrEmpty(_dynamicIbstance.qwerty));
      _updater("C5C77AEC - 2C1A - 4D33 - 9BCC - 68318A9BC8E9");
      Assert.AreEqual<string>("C5C77AEC - 2C1A - 4D33 - 9BCC - 68318A9BC8E9", _dynamicIbstance.qwerty);
    }
  }
}