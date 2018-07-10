
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tempuri.org.UA.Examples.BoilerType;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.Model
{

  [TestClass]
  public class CustomControllerStateUnitTest
  {

    [TestMethod]
    public void ConstructorTest()
    {
      CustomControllerState _controller = new CustomControllerState(null, BrowseNames.CustomController);
      Assert.IsNotNull(_controller.BrowseName);
      Assert.IsNotNull(_controller.ControlOut);
      Assert.IsNotNull(_controller.DescriptionX);
      Assert.IsNotNull(_controller.Input1);
      Assert.IsNotNull(_controller.Input2);
      Assert.IsNotNull(_controller.Input3);
      Assert.IsNull(_controller.Parent);
    }

  }
}
