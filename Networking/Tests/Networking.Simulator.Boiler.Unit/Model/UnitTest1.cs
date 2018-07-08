
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
    public void ConstructorTest()
    {
      BoilerState _boilerState = new BoilerState(null);
      Assert.IsNull(_boilerState.BrowseName);
      Assert.IsNull(_boilerState.CustomController);
      Assert.IsNull(_boilerState.Drum);
      Assert.IsNull(_boilerState.FlowController);
      Assert.IsNull(_boilerState.InputPipe);
      Assert.IsNull(_boilerState.LevelController);
      Assert.IsNull(_boilerState.OutputPipe);
      Assert.IsNull(_boilerState.Parent);
      Assert.IsNull(_boilerState.Simulation);
      Assert.AreEqual<NodeClass>(NodeClass.Object_1, _boilerState.NodeClass);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _boilerState.ChangeMasks);
    }
  }
}
