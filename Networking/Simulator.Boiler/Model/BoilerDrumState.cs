
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace tempuri.org.UA.Examples.BoilerType
{
  partial class BoilerDrumState
  {
    public BoilerDrumState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.LevelIndicator = new LevelIndicatorState(this, BrowseNames.LevelIndicator);
    }
  }
}
