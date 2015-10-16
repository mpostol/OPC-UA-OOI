
using System;
using System.ComponentModel;

namespace UAOOI.SemanticData.DataManagement.DataRepository
{
  /// <summary>
  /// Class ProducerBinding - provides a basic implementation of the <see cref="IProducerBinding"/> interface.
  /// It is used by the producer to get data from data repository.
  /// </summary>
  /// <typeparam name="type">The type of the object in the repository.</typeparam>
  public class ProducerBinding<type> : Binding<type>, IProducerBinding
  {

    #region constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerBinding{type}" /> class.
    /// </summary>
    /// <param name="valueName">Name of the "repository group" and "variable" separated by "."</param>
    /// <remarks>The <see cref="ProducerBinding{type}.GetReadValueDelegate" /> that captures a delegate used to assign new value to local variable in the data repository.</remarks>
    protected ProducerBinding(string valueName) : this(valueName, () => default(type)) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="ProducerBinding{type}" /> class.
    /// </summary>
    /// <param name="valueName">Name of the variable in the repository that is placeholder of the value.</param>
    /// <param name="getValue">Captures a delegate used to assign new value to local resources.</param>
    public ProducerBinding(string valueName, Func<type> getValue)
    {
      GetReadValueDelegate = getValue;
      m_VariableName = valueName;
    }
    #endregion

    #region IProducerBinding
    /// <summary>
    /// Gets a value indicating whether the new value is available in the repository.
    /// </summary>
    /// <value><c>true</c> if the new value is available in repository; otherwise, <c>false</c>.</value>
    bool IProducerBinding.NewValue
    {
      get
      {
        return b_NewValue;
      }
    }
    /// <summary>
    /// Gets the new value and resets the flag <see cref="IProducerBinding.NewValue" />.
    /// </summary>
    /// <returns>Current value in the repository <see cref="System.Object" />.</returns>
    object IProducerBinding.GetFromRepository()
    {
      b_NewValue = false;
      if (this.m_Converter == null)
        return GetReadValueDelegate();
      else
        return (type)m_Converter.ConvertBack(GetReadValueDelegate(), m_TargetType, m_Parameter, m_Culture);
    }
    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region API
    /// <summary>
    /// Must be called by the repository logic to signal that the new value is available in the repository.
    /// </summary>
    public void OnNewValue()
    {
      PropertyChanged.RaiseHandler<bool>(true, ref b_NewValue, "m_VariableName", this);
    }
    #endregion

    #region private
    /// <summary>
    /// Gets the delegate implementing functionality to read value from repository delegate.
    /// </summary>
    /// <value>The <see cref="Func{type}"/> delegate used to read value from repository.</value>
    protected virtual Func<type> GetReadValueDelegate { private set; get; }
    private bool b_NewValue;
    private string m_VariableName;
    #endregion

  }
}
