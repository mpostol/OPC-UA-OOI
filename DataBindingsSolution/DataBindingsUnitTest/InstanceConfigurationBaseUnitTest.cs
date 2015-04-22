using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAS.UA.IServerConfiguration;

namespace CAS.UAOOI.DataBindings.UnitTest
{
  [TestClass]
  public class InstanceConfigurationBaseUnitTest
  {

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void ClearConfigurationTestMethod()
    {
      IInstanceConfiguration _ic = new CAS.UAOOI.DataBindings.InstanceConfigurationBase();
      Assert.IsNotNull(_ic);
      _ic.ClearConfiguration();
    }

    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void EditTestMethod()
    {
      IInstanceConfiguration _ic = new CAS.UAOOI.DataBindings.InstanceConfigurationBase();
      Assert.IsNotNull(_ic);
      _ic.Edit();
    }

  }
}
