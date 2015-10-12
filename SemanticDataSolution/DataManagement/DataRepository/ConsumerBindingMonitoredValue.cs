
using System;
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement.DataRepository
{
  /// <summary>
  /// Class ConsumerBindingMonitoredValue. This class cannot be inherited.
  /// It is helper class that implements a property ready to be used in the ViewModel class according to the MVVM pattern.
  /// The view model is an abstraction of the view that exposes public properties and commands.
  /// </summary>
  /// <typeparam name="type">The type of property value.</typeparam>
  public sealed class ConsumerBindingMonitoredValue<type> : ConsumerBinding<type>, INotifyPropertyChanged
  {

    #region Model View ViewModel implementation
    /// <summary>
    /// Gets or sets the value. It is value holder to be used in the ViewModel class according to the MVVM pattern. 
    /// The view model is an abstraction of the view that exposes public properties and commands.
    /// </summary>
    /// <value>The value.</value>
    public type Value
    {
      get
      {
        return b_Value;
      }
      set
      {
        PropertyChanged.RaiseHandler<type>(value, ref b_Value, "Value", this);
      }
    }
    #endregion

    #region INotifyPropertyChanged
    /// <summary>
    /// Occurs when a property value changes. It is required for the ViewModel class according to the MVVM pattern. 
    /// The view model is an abstraction of the view that exposes public properties and commands.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region ConsumerBinding
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerBindingMonitoredValue{type}" /> class.
    /// It is used if the GetActionDelegate of teh base class is overridden.
    /// </summary>
    public ConsumerBindingMonitoredValue()
      : base()
    { }
    /// <summary>
    /// Gets or sets the get a delegate encapsulating operation called to assign new value to the destination variable by the binding machine.
    /// </summary>
    /// <value>The get action delegate.</value>
    protected override Action<type> GetActionDelegate
    {
      get
      {
        return x => Value = x;
      }
      set
      {
        base.GetActionDelegate = value;
      }
    }
    #endregion

    #region private
    private type b_Value;
    #endregion

  }
}
