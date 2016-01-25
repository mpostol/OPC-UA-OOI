
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.UnitTest
{
  [TestClass]
  public class MainWindowViewModelUnitTest
  {
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitialize]
    public void Initialize()
    {
      GalaSoft.MvvmLight.Threading.DispatcherHelper.Initialize();
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_MainWindowViewModelUnitTest")]
    public void CreatorTestMethod1()
    {
      MainWindowViewModel _vm = new MainWindowViewModel();
      Assert.IsFalse(_vm.MulticastGroupSelection);
      Assert.IsNull(_vm.DebugGetMulticastGroupIPAddress());
      Assert.AreEqual<string>("239.255.255.1", _vm.MulticastGroup);
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_MainWindowViewModelUnitTest")]
    public void DefaultIPTestMethod1()
    {
      const string _defaultIPAddress = "239.255.255.1";
      MainWindowViewModel _vm = new MainWindowViewModel();
      Assert.IsFalse(_vm.MulticastGroupSelection);
      Assert.IsNull(_vm.DebugGetMulticastGroupIPAddress());
      Assert.AreEqual<string>(_defaultIPAddress, _vm.MulticastGroup);
      _vm.MulticastGroupSelection = true;
      Assert.IsNotNull(_vm.DebugGetMulticastGroupIPAddress());
      Assert.AreEqual<string>(_defaultIPAddress, _vm.DebugGetMulticastGroupIPAddress().ToString());
      Assert.IsTrue(_vm.MulticastGroupSelection);
      _vm.MulticastGroup = "239.255.255.999";
      Assert.IsTrue(_vm.MulticastGroupSelection);
      Assert.IsNull(_vm.DebugGetMulticastGroupIPAddress());
      Assert.IsFalse(_vm.MulticastGroupSelection);
    }

  }
}
