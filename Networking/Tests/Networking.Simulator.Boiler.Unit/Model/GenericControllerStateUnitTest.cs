
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tempuri.org.UA.Examples.BoilerType;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.Model
{
  [TestClass]
  public class GenericControllerStateUnitTest
  {
    [TestMethod]
    [ExpectedExceptionAttribute(typeof(System.NotImplementedException))]
    public void ConstructorTest()
    {
      GenericControllerState _controller = new GenericControllerState(null);
      Assert.IsNull(_controller.BrowseName);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _controller.ChangeMasks);
      Assert.IsNull(_controller.ControlOut);
      Assert.IsNull(_controller.Measurement);
      Assert.IsNull(_controller.Parent);
      Assert.IsNull(_controller.SetPoint);
    }
    [TestMethod]
    public void Constructor2Test()
    {
      GenericControllerState _controller = new GenericControllerState(null, nameof(GenericControllerState));
      Assert.IsNotNull(_controller.BrowseName);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Children, _controller.ChangeMasks);
      Assert.IsNotNull(_controller.ControlOut);
      Assert.IsNotNull(_controller.Measurement);
      Assert.IsNull(_controller.Parent);
      Assert.IsNotNull(_controller.SetPoint);
    }
    [TestMethod]
    public void ParentsTest()
    {
      GenericControllerState _controller = new GenericControllerState(null, "browse name");
      Assert.IsNotNull(_controller.BrowseName);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Children, _controller.ChangeMasks);
      Assert.IsNull(_controller.Parent);
      _controller.ControlOut = new PropertyState<double>(_controller, "ControlOut") { Value = 0.0 };
      Assert.AreSame(_controller, _controller.ControlOut.Parent);
      _controller.Measurement = new PropertyState<double>(_controller, "Measurement") { Value = 0.0 };
      Assert.AreSame(_controller, _controller.Measurement.Parent);
      _controller.SetPoint = new PropertyState<double>(_controller, "SetPoint") { Value = 0.0 };
      Assert.AreSame(_controller, _controller.SetPoint.Parent);
    }
  }
}
