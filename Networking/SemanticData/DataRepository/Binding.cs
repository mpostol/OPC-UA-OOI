
using System;
using System.Globalization;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.DataRepository
{

  /// <summary>
  /// Class Binding - a generic implementation of the <see cref="IBinding"/> interface. 
  /// The instance of this class is used to update a destination variable by an owner of this object.
  /// It captures an association targeted a variable that is to be updated by the user of this instance.
  /// It is assumed that the repository implements the <see cref="IBindingFactory"/> interface and is the factory of this instance.
  /// </summary>
  public class Binding : IBinding
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="Binding" /> class.
    /// </summary>
    /// <param name="encoding">The <see cref="BuiltInType"/>of the message field encoding.</param>
    public Binding(UATypeInfo encoding)
    {
      m_MessageEncoding = encoding;
    }
    #endregion

    #region IBinding
    /// <summary>
    /// Gets the type of the message field encoding.
    /// </summary>
    /// <value>The <see cref="BuiltInType" />of the message field encoding.</value>
    public UATypeInfo Encoding { get { return m_MessageEncoding; } }
    /// <summary>
    /// Sets the converter, which is used to provide a way to apply custom logic to a binding.
    /// </summary>
    /// <value>The converter as an instance of the <see cref="IValueConverter" />.</value>
    IValueConverter IBinding.Converter { set { Converter = value; } }
    /// <summary>
    /// Gets or sets an optional parameter to be used in the converter logic or serialization process.
    /// </summary>
    /// <value>The parameter to be used by the <see cref="IBinding.Converter" /> or by serialization process.</value>
    object IBinding.Parameter
    {
      set { Parameter = value; }
      get { return Parameter; }
    }
    Object IBinding.FallbackValue { set { FallbackValue = value; } }
    /// <summary>
    /// Sets the culture of the conversion.
    /// </summary>
    /// <value>The culture as an instance of the <see cref="CultureInfo" /> to be used by the <see cref="IBinding.Converter" />.</value>
    CultureInfo IBinding.Culture
    {
      set { Culture = value; }
    }
    /// <summary>
    /// Marks the process value enabled - signal that the update of the value is expected.
    /// </summary>
    void IBinding.OnEnabling()
    {
      RaiseHandlerState(HandlerState.Operational);
    }
    /// <summary>
    /// Marks the process value disabled - signal that the value will not be updated.
    /// </summary>
    void IBinding.OnDisabling()
    {
      RaiseHandlerState(HandlerState.Disabled);
    }
    #endregion

    #region public API
    /// <summary>
    /// Gets the state of the handler.
    /// </summary>
    /// <value>The state of the handler.</value>
    public HandlerState HandlerState { get { return m_HandlerState; } }
    /// <summary>
    /// Occurs when state changes].
    /// </summary>
    public event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    #endregion

    #region private
    private UATypeInfo m_MessageEncoding;
    /// <summary>
    /// Sets the converter, which is used to provide a way to apply custom logic to a binding.
    /// </summary>
    /// <value>The converter as an instance of the <see cref="IValueConverter" />.</value>
    protected IValueConverter Converter
    {
      get; private set;
    }
    /// <summary>
    /// Gets the culture.
    /// </summary>
    /// <value>The culture.</value>
    protected CultureInfo Culture { get; private set; }
    /// <summary>
    /// Sets an optional parameter to be used in the converter logic.
    /// </summary>
    /// <value>The parameter to be used by the <see cref="IBinding.Converter" />.</value>
    protected object Parameter
    {
      get; private set;
    }
    public object FallbackValue { get; private set; }
    private HandlerState m_HandlerState = HandlerState.Operational;
    private void RaiseHandlerState(HandlerState state)
    {
      EventHandler<AssociationStateChangedEventArgs> _hc = StateChangedEventHandler;
      if (_hc != null)
        _hc(this, new AssociationStateChangedEventArgs(state));
    }
    #endregion
  }

}
