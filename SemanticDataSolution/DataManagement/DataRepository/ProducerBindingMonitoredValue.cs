using System;

namespace UAOOI.SemanticData.DataManagement.DataRepository
{
  /// <summary>
  /// Class ProducerBindingMonitoredValue - it implements the <see cref="ProducerBinding{type}"/> as a placeholder of the value to send over the network by the producer.
  /// </summary>
  /// <typeparam name="type">The type of the object in the repository.</typeparam>
  public sealed class ProducerBindingMonitoredValue<type> : ProducerBinding<type>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerBinding{type}" /> class.
    /// </summary>
    /// <param name="valueName">Name of the "repository group" and "variable" separated by "."</param>
    public ProducerBindingMonitoredValue(string valueName)
      : base(valueName)
    { }
    /// <summary>
    /// Gets or sets the monitored value - it is placeholder of the variable in the repository.
    /// </summary>
    /// <value>The monitored value.</value>
    public type MonitoredValue
    {
      get
      {
        return b_MyProperty;
      }
      set
      {
        if (Object.Equals(b_MyProperty, value))
          return;
        b_MyProperty = value;
        OnNewValue();
      }
    }

    /// <summary>
    /// Gets the delegate implementing functionality to read value from repository delegate.
    /// </summary>
    /// <value>The <see cref="Func{type}" /> delegate used to read value from repository.</value>
    protected override Func<type> GetReadValueDelegate
    {
      get
      {
        return () => MonitoredValue;
      }
    }
    private type b_MyProperty;

  }

}


