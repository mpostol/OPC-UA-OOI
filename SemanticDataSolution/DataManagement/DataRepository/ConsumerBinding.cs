
using System;

namespace UAOOI.SemanticData.DataManagement.DataRepository
{

  /// <summary>
  /// Class ConsumerBinding - provide a basic implementation of the <see cref="IConsumerBinding" /> interface.
  /// It is an helper class to construct an object used by the consumer to save the data in the data repository.
  /// </summary>
  /// <typeparam name="type">The type of the type.</typeparam>
  public class ConsumerBinding<type> : Binding<type>, IConsumerBinding
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerBinding{type}"/> class.
    /// </summary>
    /// <param name="assign">Captures a delegate used to assign new value to local resources.</param>
    public ConsumerBinding(Action<type> assign)
    {
      GetActionDelegate = assign;
    }

    #region IConsumerBinding
    /// <summary>
    /// Assigns the <paramref name="value" /> to the associated variable hosted by the target repository.
    /// </summary>
    /// <param name="value">The value to be assigned to the precess variable.</param>
    void IConsumerBinding.Assign2Repository(object value)
    {
      if (this.m_Converter == null)
        GetActionDelegate((type)value);
      else
        GetActionDelegate((type)m_Converter.Convert(value, m_TargetType, m_Parameter, m_Culture));
    }
    #endregion

    #region private
    protected virtual Action<type> GetActionDelegate { set; get; }
    protected ConsumerBinding()
      : this(x => { })
    { }
    #endregion

  }
}
