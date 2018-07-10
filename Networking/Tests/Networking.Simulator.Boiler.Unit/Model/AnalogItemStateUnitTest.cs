using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.Model
{
  [TestClass]
  public class AnalogItemStateUnitTest
  {
    [TestMethod]
    public void ComstructorTest()
    {
      AnalogItemState<double> _item = new AnalogItemState<double>(null, "browseName", ModelExtensions.CreateRange(0, 1), 0.5);
      Assert.AreEqual<string>("browseName", _item.BrowseName.Name);
      Assert.IsFalse(_item.BrowseName.NamespaceIndexSpecified);
      //EURange
      Assert.IsNotNull(_item.EURange);
      Assert.AreEqual<string>(nameof(_item.EURange), _item.EURange.BrowseName.Name);
      Assert.AreEqual<double>(1, _item.EURange.Value.High);
      Assert.AreEqual<double>(0, _item.EURange.Value.Low);
      //Value
      Assert.IsNotNull(_item.Value);
      Assert.AreEqual<double>(0.5, _item.Value);
      Assert.AreEqual<NodeClass>(NodeClass.Variable_2, _item.NodeClass);
      Assert.IsNull(_item.Parent);
      Assert.AreEqual<double>(0.5, _item.Value);
    }
  }
}
