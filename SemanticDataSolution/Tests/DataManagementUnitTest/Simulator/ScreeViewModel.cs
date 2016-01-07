
using System;
using System.ComponentModel;
using UAOOI.SemanticData.DataManagement.DataRepository;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest.Simulator
{

  /// <summary>
  /// Class ScreeViewModel - this class demonstrates how to create bindings to the properties that are holders of values in the Model View ViewModel pattern.
  /// </summary>
  internal class ScreeViewModel : INotifyPropertyChanged
  {

    #region API
    /// <summary>
    /// Helper method that creates the consumer binding. 
    /// </summary>
    /// <param name="variableName">Name of the variable.</param>
    /// <returns>IConsumerBinding.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">variableName</exception>
    public IConsumerBinding GetConsumerBinding(string variableName, BuiltInType encoding)
    {
      UATypeInfo _uaTypeInfo = new UATypeInfo(encoding);
      if (variableName == "Value1")
      {
        Value1 = new ConsumerBindingMonitoredValue<string>(_uaTypeInfo);
        return Value1;
      }
      else if (variableName == "Value2")
      {
        Value2 = new ConsumerBindingMonitoredValue<double>(_uaTypeInfo);
        return Value2;
      }
      throw new ArgumentOutOfRangeException("variableName");
    }
    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region ModelView implementation
    public ConsumerBindingMonitoredValue<string> Value1
    {
      get
      {
        return b_Value1;
      }
      set
      {
        PropertyChanged.RaiseHandler<ConsumerBindingMonitoredValue<string>>(value, ref b_Value1, "Value1", this);
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
    private ConsumerBindingMonitoredValue<string> b_Value1;
    private ConsumerBindingMonitoredValue<double> b_Value2;

    #endregion

  }
}
