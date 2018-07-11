
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace tempuri.org.UA.Examples.BoilerType
{
  partial class CustomControllerState
  {
    public CustomControllerState(NodeState parent, string browseName) : base(parent, browseName)
    {
      this.ControlOut = new PropertyState<double>(this, BrowseNames.ControlOut);
      this.DescriptionX = new PropertyState<LocalizedText>(this, BrowseNames.DescriptionX);
      this.Input1 = new PropertyState<double>(this, BrowseNames.Input1);
      this.Input2 = new PropertyState<double>(this, BrowseNames.Input2);
      this.Input3 = new PropertyState<double>(this, BrowseNames.Input3);
    }
  }
}
