
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace tempuri.org.UA.Examples.BoilerType
{
  partial class BoilerDrumState
  {
    public BoilerDrumState(NodeState parent, QualifiedName browseName) : this(parent)
    {
      BrowseName = browseName;
      this.LevelIndicator = new LevelIndicatorState(this);
      this.LevelIndicator.Initialize(BrowseNames.LevelIndicator);
    }
  }
}
