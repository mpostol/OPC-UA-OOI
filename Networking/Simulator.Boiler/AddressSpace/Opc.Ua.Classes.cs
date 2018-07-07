namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{

  public partial class DataItemState : BaseDataVariableState
  {
    public DataItemState(NodeState parent) : base(parent) { }
  }
  public class AnalogItemState<T> : DataItemState
  {

    #region Constructors
    /// <summary>
    /// Initializes the instance with its default attribute values.
    /// </summary>
    public AnalogItemState(NodeState parent) : base(parent)
    {
      Value = default(T);
    }
    #endregion

    #region Public Members
    /// <summary>
    /// The value of the variable.
    /// </summary>
    public T Value { get; set; }
    #endregion
  }

}
