//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Windows.Input;
using UAOOI.Networking.ReferenceApplication.Core.MvvmLight;

namespace UAOOI.Networking.DataRepository.DataLogger
{
  /// <summary>
  /// Class ConsumerViewModel -custom implementation of the ViewModel for this DataLogger
  /// </summary>
  /// <seealso cref="UAOOI.Networking.ReferenceApplication.Core.MvvmLight.ObservableObject" />
  public abstract class ConsumerViewModel : ObservableObject
  {
    #region ViewModel

    /// <summary>
    /// Gets or sets the consumer received bytes.
    /// </summary>
    /// <value>The consumer received bytes.</value>
    public int ConsumerReceivedBytes
    {
      get => b_ConsumerBytesReceived;
      set
      {
        b_ConsumerBytesReceived = value;
        RaisePropertyChanged<int>("ConsumerReceivedBytes", b_ConsumerBytesReceived, value);
      }
    }

    /// <summary>
    /// Gets or sets the number of consumer received frames .
    /// </summary>
    /// <value>The consumer frames received.</value>
    public int ConsumerFramesReceived
    {
      get => b_ConsumerFramesReceived;
      set
      {
        b_ConsumerFramesReceived = value;
        RaisePropertyChanged<int>("ConsumerFramesReceived", b_ConsumerFramesReceived, value);
      }
    }

    /// <summary>
    /// Gets or sets the consumer update configuration command.
    /// </summary>
    /// <value>The consumer update configuration <see cref="ICommand" />.</value>
    public ICommand ConsumerUpdateConfiguration //TODO Remove reference of ConsumerDataManagementSetup System.Windows  #239
    {
      get => b_ConsumerUpdateConfiguration;
      set
      {
        b_ConsumerUpdateConfiguration = value;
        RaisePropertyChanged<ICommand>("ConsumerUpdateConfiguration", b_ConsumerUpdateConfiguration, value);
      }
    }

    /// <summary>
    /// Gets or sets the last consumer error message.
    /// </summary>
    /// <value>The consumer error message.</value>
    public string ConsumerErrorMessage
    {
      get => b_ConsumerErrorMessage;
      set
      {
        b_ConsumerErrorMessage = value;
        RaisePropertyChanged<string>("ConsumerErrorMessage", b_ConsumerErrorMessage, value);
      }
    }

    /// <summary>
    /// Add the message to the Log on the UI.
    /// </summary>
    /// <param name="message">The message to be added to the log.</param>
    protected internal abstract void Trace(string message);

    #endregion ViewModel

    #region API

    internal void ChangeProducerCommand(Action action)
    {
      ConsumerUpdateConfiguration = new DelegateCommand(action);
    }

    #endregion API

    #region private

    private int b_ConsumerBytesReceived;
    private int b_ConsumerFramesReceived;
    private ICommand b_ConsumerUpdateConfiguration;
    private string b_ConsumerErrorMessage;

    #endregion private
  }
}