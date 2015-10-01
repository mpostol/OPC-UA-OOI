
namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Interface IAssociationState - encapsulates state of the hosting instance. 
  /// The provided functionality behavior depends on the current value returned by the <see cref="IAssociationState.State"/> property.
  /// </summary>
  public interface IAssociationState
  {
    /// <summary>
    /// Gets the current state of the <see cref="Association"/> instance.
    /// </summary>
    /// <value>The state.</value>
    HandlerState State { get; }
    /// <summary>
    /// This method is used to enable a configured <see cref="IAssociation"/> object. If a normal operation is possible, the state changes into <see cref="HandlerState.Operational"/> state. 
    /// In the case of an error situation, the state changes into <see cref="HandlerState.Error"/>. The operation is rejected if the current <paramref name="HandlerState"/>  is not <see cref="HandlerState.Disabled"/>.
    /// </summary>
    void Enable();
    /// <summary>
    /// This method is used to disable an already enabled <see cref="IAssociation"/> object.
    /// This method call shall be rejected if the current State is <see cref="HandlerState.Disabled"/> or <see cref="HandlerState.NoConfiguration"/>.
    /// </summary>
    void Disable();
  }
}
