
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.UnitTest
{
  [TestClass]
  public class MainWindowViewModelUnitTest
  {
    [TestMethod]
    [TestCategory("ReferenceApplication_MainWindowViewModelUnitTest")]
    public void CreatorTestMethod1()
    {
      MainWindowViewModel _vm = new MainWindowViewModel();
      Assert.IsFalse(_vm.MulticastGroupSelection);
      Assert.IsNull(_vm.MulticastGroupIPAddress);
      Assert.AreEqual<string>("139.255.255.1", _vm.MulticastGroup);
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_MainWindowViewModelUnitTest")]
    public void DefaultIPTestMethod1()
    {
      const string _defaultIPAddress = "139.255.255.1";
      MainWindowViewModel _vm = new MainWindowViewModel();
      Assert.IsFalse(_vm.MulticastGroupSelection);
      Assert.IsNull(_vm.MulticastGroupIPAddress);
      Assert.AreEqual<string>(_defaultIPAddress, _vm.MulticastGroup);
      _vm.MulticastGroupSelection = true;
      Assert.IsNotNull(_vm.MulticastGroupIPAddress);
      Assert.AreEqual<string>(_defaultIPAddress, _vm.MulticastGroupIPAddress.ToString());
      Assert.IsTrue(_vm.MulticastGroupSelection);
      _vm.MulticastGroup = "139.255.255.999";
      Assert.IsTrue(_vm.MulticastGroupSelection);
      Assert.IsNull(_vm.MulticastGroupIPAddress);
      Assert.IsFalse(_vm.MulticastGroupSelection);
    }

  }
}
