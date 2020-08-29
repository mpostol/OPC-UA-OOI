//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

namespace UAOOI.Networking.DataRepository.AzureGateway.AzureInterconnection
{
  /// <summary>
  /// Class CommunicationContext - implements Azure communication state machine.
  /// Implements the <see cref="IStateBase" />
  /// </summary>
  /// <seealso cref="IStateBase" />
  internal class CommunicationContext : IStateBase
  {
    #region constructor

    internal CommunicationContext(StateBase state)
    {
      SetContext(state);
    }

    #endregion constructor

    #region IStateBase

    public void Register()
    {
      _currenyState.Register();
    }

    public void Connect()
    {
      _currenyState.Connect();
    }

    public void TransferData(IDTOProvider dataProvider, string repositoryGroup)
    {
      _currenyState.TransferData(dataProvider, repositoryGroup);
    }

    public void DisconnectRequest()
    {
      _currenyState.DisconnectRequest();
    }

    #endregion IStateBase

    #region private

    internal abstract class StateBase : IStateBase
    {
      #region constructors

      public StateBase(CommunicationContext communicationContext)
      {
        _context = communicationContext;
      }

      #endregion constructors

      #region IStateBase

      public abstract void Register();

      public abstract void Connect();

      public abstract void TransferData(IDTOProvider dataProvider, string repositoryGroup);

      public abstract void DisconnectRequest();

      #endregion IStateBase

      protected void TransitionTo(StateBase state)
      {
        _context.SetContext(this);
      }

      protected CommunicationContext _context;
    }

    private void SetContext(StateBase stateBase)
    {
      _currenyState = stateBase;
    }

    private StateBase _currenyState = null;

    #endregion private
  }
}