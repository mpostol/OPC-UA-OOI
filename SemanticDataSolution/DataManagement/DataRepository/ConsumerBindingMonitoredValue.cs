
using System;
using System.ComponentModel;
using System.Linq;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

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

    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerBindingMonitoredValue{type}" /> class.
    /// It is used if the GetActionDelegate of teh base class is overridden.
    /// </summary>
    public ConsumerBindingMonitoredValue(BuiltInType targetType)
      : base(targetType)
    { }
    /// <summary>
    /// Gets or sets the get a delegate encapsulating operation called to assign new value to the destination variable by the binding machine.
    /// </summary>
    /// <value>The get action delegate.</value>
    protected override Action<type> AssignValueToRepository
    {
      get
      {
        return x => Value = x;
      }
      set
      {
        base.AssignValueToRepository = value;
      }
    }
    #endregion

    #region override object
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      switch (Encoding)
      {
        case BuiltInType.Null:
        case BuiltInType.Boolean:
        case BuiltInType.SByte:
        case BuiltInType.Byte:
        case BuiltInType.Int16:
        case BuiltInType.UInt16:
        case BuiltInType.Int32:
        case BuiltInType.UInt32:
        case BuiltInType.Int64:
        case BuiltInType.UInt64:
        case BuiltInType.Float:
        case BuiltInType.Double:
        case BuiltInType.String:
        case BuiltInType.DateTime:
        case BuiltInType.Guid:
        case BuiltInType.XmlElement:
        case BuiltInType.NodeId:
        case BuiltInType.ExpandedNodeId:
        case BuiltInType.StatusCode:
        case BuiltInType.QualifiedName:
        case BuiltInType.LocalizedText:
        case BuiltInType.ExtensionObject:
        case BuiltInType.DataValue:
        case BuiltInType.Variant:
        case BuiltInType.DiagnosticInfo:
        case BuiltInType.Enumeration:
          return Value.ToString();
        case BuiltInType.ByteString:
          byte[] _value = (byte[])(object)Value;
          return $"[{String.Join(", ", new ArraySegment<byte>(_value, 0, Math.Min(_value.Length, 80)).Select<byte, string>(x => x.ToString("X")).ToArray<string>())}]";
      }
      return base.ToString();
    }
    #endregion

    #region private
    private type b_Value;
    #endregion

  }
}
