
using System;
using System.ComponentModel;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{

  /// <summary>
  /// Class MainWindowModelView - this class demonstrates how to create bindings to the properties that are holders of values in the 
  /// Model View ViewModel pattern.
  /// </summary>
  internal class MainWindowModelView : INotifyPropertyChanged, IModelViewBindingFactory, IProducerModelView
  {

    public MainWindowModelView()
    {
      b_UDPPort = Properties.Settings.Default.UDPPort;
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

    #region User INterface - ModelView implementation
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
    //private part
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
    private int b_BytesSent;
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
    private int b_PackagesSent;
    public string RemoteHost
    {
      get
      {
        return b_RemoteHost;
      }
      set
      {
        PropertyChanged.RaiseHandler<string>(value, ref b_RemoteHost, "RemoteHost", this);
      }
    }
    private string b_RemoteHost;
    public int RemotePort
    {
      get
      {
        return b_RemotePort;
      }
      set
      {
        PropertyChanged.RaiseHandler<int>(value, ref b_RemotePort, "RemotePort", this);
      }
    }
    private int b_RemotePort;
    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

  }

}
