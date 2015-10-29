
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{

  /// <summary>
  /// Class MainWindowModelView - this class demonstrates how to create bindings to the properties that are holders of OPC UA values in the 
  /// Model View ViewModel pattern.
  /// </summary>
  internal class MainWindowModelView : INotifyPropertyChanged, IModelViewBindingFactory, IProducerModelView, IConsumerModelView
  {

    public MainWindowModelView()
    {
      b_UDPPort = Properties.Settings.Default.UDPPort;
      b_RemoteHost = Properties.Settings.Default.RemoteHostName;
      b_RemotePort = Properties.Settings.Default.RemoteUDPPortNumber;
    }

    #region API
    /// <summary>
    /// Helper method that creates the consumer binding. 
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <returns>IConsumerBinding.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    public IConsumerBinding GetConsumerBinding(string variableName)
    {
      if (variableName == "Value1")
      {
        Value1 = new ConsumerBindingMonitoredValue<DateTime>();
        return Value1;
      }
      else if (variableName == "Value2")
      {
        Value2 = new ConsumerBindingMonitoredValue<double>();
        return Value2;
      }
      throw new ArgumentOutOfRangeException("variableName");
    }
    #endregion

    #region Consumer User Interface - ModelView implementation
    /// <summary>
    /// Gets or sets the value1 - an example of OPC UA data binded to the <see cref="System.Windows.Controls.TextBox"/>.
    /// </summary>
    /// <value>The value1 represented by the <see cref="ConsumerBindingMonitoredValue"/>.</value>
    public ConsumerBindingMonitoredValue<DateTime> Value1
    {
      get
      {
        return b_Value1;
      }
      set
      {
        PropertyChanged.RaiseHandler<ConsumerBindingMonitoredValue<DateTime>>(value, ref b_Value1, "Value1", this);
      }
    }
    /// <summary>
    /// Gets or sets the value2 - an example of OPC UA data binded to the <see cref="System.Windows.Controls.TextBox"/>.
    /// </summary>
    /// <value>The value2.</value>
    public ConsumerBindingMonitoredValue<double> Value2
    {
      get
      {
        return b_Value2;
      }
      set
      {
        PropertyChanged.RaiseHandler<ConsumerBindingMonitoredValue<double>>(value, ref b_Value2, "Value2", this);
      }
    }
    /// <summary>
    /// Gets or sets the UDP port.
    /// </summary>
    /// <value>The UDP port.</value>
    public int UDPPort
    {
      get
      {
        return b_UDPPort;
      }
      set
      {
        if (PropertyChanged.RaiseHandler<int>(value, ref b_UDPPort, "UDPPort", this))
          Properties.Settings.Default.UDPPort = value;
      }
    }
    public int ConsumerBytesReceived
    {
      get
      {
        return b_ConsumerBytesReceived;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_ConsumerBytesReceived, "ConsumerBytesReceived", this);
      }
    }
    public int ConsumerFramesReceived
    {
      get
      {
        return b_ConsumerFramesReceived;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_ConsumerFramesReceived, "ConsumerFramesReceived", this);
      }
    }
    public ICommand ConsumerUpdateConfiguration
    {
      get
      {
        return b_ConsumerUpdateConfiguration;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_ConsumerUpdateConfiguration, "ConsumerUpdateConfiguration", this);
      }
    }
    public string ConsumerErrorMessage
    {
      get
      {
        return b_ConsumerErrorMessage;
      }
      set
      {
        PropertyChanged.RaiseHandler<string>(value, ref b_ConsumerErrorMessage, "ConsumerErrorMessage", this);
      }
    }
    public ObservableCollection<string> ConsumerLog
    {
      get
      {
        return b_ConsumerLog;
      }
      set
      {
        PropertyChanged.RaiseHandler<ObservableCollection<string>>(value, ref b_ConsumerLog, "ConsumerLog", this);
      }
    }
    //private part
    private ObservableCollection<string> b_ConsumerLog;
    private string b_ConsumerErrorMessage;
    private ICommand b_ConsumerUpdateConfiguration;
    private int b_ConsumerFramesReceived;
    private int b_ConsumerBytesReceived;
    private int b_UDPPort;
    private ConsumerBindingMonitoredValue<DateTime> b_Value1;
    private ConsumerBindingMonitoredValue<double> b_Value2;
    #endregion

    #region Producer user interface
    public int BytesSent
    {
      get
      {
        return b_BytesSent;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_BytesSent, "BytesSent", this);
      }
    }
    public int PackagesSent
    {
      get
      {
        return b_PackagesSent;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_PackagesSent, "PackagesSent", this);
      }
    }
    public string RemoteHost
    {
      get
      {
        return b_RemoteHost;
      }
      set
      {
        if (PropertyChanged.RaiseHandler<string>(value, ref b_RemoteHost, "RemoteHost", this))
          Properties.Settings.Default.RemoteHostName = value;
      }
    }
    public int RemotePort
    {
      get
      {
        return b_RemotePort;
      }
      set
      {
        if (PropertyChanged.RaiseHandler<int>(value, ref b_RemotePort, "RemotePort", this))
          Properties.Settings.Default.RemoteUDPPortNumber = value;
      }
    }
    public ICommand ProducerRestart
    {
      get
      {
        return b_ProducerRestart;
      }
      set
      {
        PropertyChanged.RaiseHandler<ICommand>(value, ref b_ProducerRestart, "ProducerRestart", this);
      }
    }
    public string ProducerErrorMessage
    {
      get
      {
        return b_ProducerErrorMessage;
      }
      set
      {
        PropertyChanged.RaiseHandler<string>(value, ref b_ProducerErrorMessage, "ProducerErrorMessage", this);
      }
    }
    //private part
    private int b_BytesSent;
    private int b_PackagesSent;
    private string b_RemoteHost;
    private int b_RemotePort;
    private ICommand b_ProducerRestart;
    private string b_ProducerErrorMessage;


    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

  }

}
