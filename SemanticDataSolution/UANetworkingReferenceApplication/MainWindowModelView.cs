
using System;
using System.ComponentModel;
using System.Threading;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication
{

  /// <summary>
  /// Class MainWindowModelView - this class demonstrates how to create bindings to the properties that are holders of values in the 
  /// Model View ViewModel pattern.
  /// </summary>
  internal class MainWindowModelView : INotifyPropertyChanged, IModelViewBindingFactory
  {

    public MainWindowModelView()
    {
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

    #region ViewModel implementation
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
    private ConsumerBindingMonitoredValue<DateTime> b_Value1;
    private ConsumerBindingMonitoredValue<double> b_Value2;
    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

  }
}
