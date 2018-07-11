
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using tempuri.org.UA.Examples.BoilerType;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.Model
{
  [TestClass]
  public class BoilerDrumStateUnitTest
  {
    [TestMethod]
    [ExpectedException(typeof(NotImplementedException))]
    public void ConstructorTestMethod()
    {
      BoilerDrumState _drum = new BoilerDrumState(null);
      Assert.IsNull(_drum.BrowseName);
      Assert.IsNull(_drum.LevelIndicator);
      Assert.IsNull(_drum.Parent);
      Assert.AreEqual<NodeClass>(NodeClass.Object_1, _drum.NodeClass);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.None, _drum.ChangeMasks);
    }
    [TestMethod]
    public void Constructor2TestMethod()
    {
      BoilerDrumState _drum = new BoilerDrumState(null, BrowseNames.Drum);
      Assert.IsNotNull(_drum.BrowseName);
      Assert.IsNotNull(_drum.LevelIndicator);
      Assert.IsNotNull(_drum.LevelIndicator.Output);
      Assert.AreEqual<NodeClass>(NodeClass.Variable_2, _drum.LevelIndicator.Output.NodeClass);
      Assert.IsNotNull(_drum.LevelIndicator.Output.Parent);
      Assert.IsNotNull(_drum.LevelIndicator.Output.Value);
      Assert.IsNotNull(_drum.LevelIndicator.Output.EURange);
      Assert.IsNotNull(_drum.LevelIndicator.Parent);
      Assert.IsNull(_drum.Parent);
      Assert.AreEqual<NodeClass>(NodeClass.Object_1, _drum.NodeClass);
      Assert.AreEqual<NodeStateChangeMasks>(NodeStateChangeMasks.Children, _drum.ChangeMasks);
    }
  }

}
