using UAOOI.Networking.SemanticData.Common;

namespace UAOOI.Networking.SemanticData.MessageHandling
{
  public abstract class MessageHandler : IMessageHandler
  {

    #region IMessageHandler
    /// <summary>
    /// Attaches to network - initialize the underlying protocol stack and establish the connection with the broker is applicable.
    /// </summary>
    /// <remarks>Depending on the message transport layer type implementation of this function varies.</remarks>
    public abstract void AttachToNetwork();
    /// <summary>
    /// Gets the state machine for the the <see cref="IMessageHandler" /> instance.
    /// </summary>
    /// <value>An object of <see cref="IAssociationState" /> providing implementation of the state machine governing this instance behavior.</value>
    public abstract IAssociationState State { get; protected set; }
    /// <summary>
    /// Gets the content mask. The content mast read from the message or provided by the writer.
    /// The order of the bits starting from the least significant bit matches the order of the data items
    /// within the data set.
    /// </summary>
    /// <value>The content mask represented as unsigned number <see cref="UInt64" />. The order of the bits starting from the least significant
    /// bit matches the order of the data items within the data set.</value>
    public abstract ulong ContentMask { get; protected set; }
    #endregion

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls
    protected virtual void Dispose(bool disposing)
    {
      if (disposedValue)
        return;
      disposedValue = true;
    }
    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion

  }
}
