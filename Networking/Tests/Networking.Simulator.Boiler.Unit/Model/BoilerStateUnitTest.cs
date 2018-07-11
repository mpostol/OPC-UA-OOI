
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tempuri.org.UA.Examples.BoilerType;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.Model
{
  [TestClass]
  public class BoilerStateUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(System.NotImplementedException))]
    public void ConstructorTest()
    {
      BoilerState _boilerState = new BoilerState(null);
    }
    [TestMethod]
    public void Constructor2Test()
    {
      BoilerState _boilerState = new BoilerState(null, "browseName");
      Assert.IsNotNull(_boilerState.BrowseName);
      Assert.AreEqual<string>("browseName", _boilerState.BrowseName.Name);
      Assert.IsFalse(_boilerState.BrowseName.NamespaceIndexSpecified);
      Assert.IsNotNull(_boilerState.CustomController);
      Assert.IsNotNull(_boilerState.Drum);
      Assert.IsNotNull(_boilerState.FlowController);
      Assert.IsNotNull(_boilerState.InputPipe);
      Assert.IsNotNull(_boilerState.LevelController);
      Assert.IsNotNull(_boilerState.OutputPipe);
      Assert.IsNull(_boilerState.Parent);
      Assert.IsNotNull(_boilerState.Simulation);
      Assert.AreEqual<NodeClass>(NodeClass.Object_1, _boilerState.NodeClass);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Children, _boilerState.ChangeMasks);
    }
  }
}
