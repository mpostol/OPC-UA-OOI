using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using UAOOI.Networking.Simulator.Boiler.Model;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;

namespace tempuri.org.UA.Examples.BoilerType
{
  public partial class LevelIndicatorState
  {
    public LevelIndicatorState(NodeState parent, QualifiedName browseName) : base(parent, browseName)
    {
      this.Output = new AnalogItemState<double>(this, BrowseNames.Output, ModelExtensions.CreateRange(0, 1), 0.5);
    }
  }
  public partial class GenericSensorState : BaseObjectState
  {
    public GenericSensorState(NodeState parent, QualifiedName browseName) : base(parent, browseName) { }
  }
}
