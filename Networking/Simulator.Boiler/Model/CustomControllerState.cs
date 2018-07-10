
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace tempuri.org.UA.Examples.BoilerType
{
  partial class CustomControllerState
  {
    public CustomControllerState(NodeState parent, string browseName) : this(parent)
    {
      this.BrowseName = browseName;
      this.ControlOut = new PropertyState<double>(this) { BrowseName = nameof(ControlOut) };
      this.DescriptionX = new PropertyState<LocalizedText>(this);
      this.Input1 = new PropertyState<double>(this);
      this.Input2 = new PropertyState<double>(this);
      this.Input3 = new PropertyState<double>(this);
    }
  }
}
