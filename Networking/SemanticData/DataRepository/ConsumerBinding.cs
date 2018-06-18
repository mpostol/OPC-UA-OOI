
using System;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Networking.SemanticData.DataRepository
{

  /// <summary>
  /// Class ConsumerBinding - provide a basic implementation of the <see cref="IConsumerBinding" /> interface.
  /// It is an helper class to construct an object used by the consumer to save the data in the data repository.
  /// </summary>
  /// <typeparam name="type">The type of the type.</typeparam>
  public class ConsumerBinding<type> : Binding, IConsumerBinding
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerBinding{type}" /> class.
    /// </summary>
    /// <param name="assign">Captures a delegate used to assign new value to local resources.</param>
    /// <param name="encoding">The <see cref="UATypeInfo"/> of the message field encoding.</param>
    public ConsumerBinding(Action<type> assign, UATypeInfo encoding) : base(encoding)
    {
      AssignValueToRepository = assign;
    }

    #region IConsumerBinding
    /// <summary>
    /// Assigns the <paramref name="value" /> to the associated variable hosted by the target repository.
    /// </summary>
    /// <param name="value">The value to be assigned to the precess variable.</param>
    void IConsumerBinding.Assign2Repository(object value)
    {
      if (this.Converter == null)
        AssignValueToRepository((type)value);
      else
        AssignValueToRepository((type)Converter.Convert(value, typeof(type), FallbackValue, Parameter, Culture));
    }
    #endregion

    #region private
    /// <summary>
    /// Gets or sets the assign value to repository delegate.
    /// </summary>
    /// <value>The assign value to repository.</value>
    protected virtual Action<type> AssignValueToRepository { set; get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsumerBinding{type}"/> class.
    /// </summary>
    /// <param name="encoding">The <see cref="UATypeInfo"/>of the message field encoding.</param>
    protected ConsumerBinding(UATypeInfo encoding)
      : this(x => { }, encoding)
    { }
    #endregion

  }
}
