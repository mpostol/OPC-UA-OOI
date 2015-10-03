
using System;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class Binding - a generic implementation of the <see cref="IBinding"/> interface. 
  /// The instance of this class is used to update a destination variable by an owner of this object.
  /// It captures an association targeted a variable that is to be updated by the user of this instance.
  /// It is assumed that the repository implements the <see cref="IBindingFactory"/> interface and is the factory of this instance.
  /// </summary>
  public class Binding<type> : IBinding
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Binding{type}"/> class.
    /// </summary>
    /// <param name="assign">This parameters captures a delegate that will be used to update the destination variable hosted by a repository. 
    /// </param>
    public Binding(Action<type> assign)
    {
      TargetType = typeof(type);
      m_Assign = assign;
    }
    #region IBinding
    /// <summary>
    /// Gets the type of the repository a variable that is to be updated using <see cref="IBinding.Assign2Repository" />.
    /// </summary>
    /// <value>The type of the repository target variable of the binding.</value>
    public Type TargetType { get; private set; }
    /// <summary>
    /// Sets the converter, which is used to provide a way to apply custom logic to a binding.
    /// </summary>
    /// <value>The converter as an instance of the <see cref="IValueConverter" />.</value>
    public IValueConverter Converter { private get; set; }
    /// <summary>
    /// Sets an optional parameter to be used in the converter logic.
    /// </summary>
    /// <value>The parameter to be used by the <see cref="IBinding.Converter" />.</value>
    public object Parameter
    {
      set;
      private get;
    }
    /// <summary>
    /// Sets the culture of the conversion.
    /// </summary>
    /// <value>The culture as an instance of the <see cref="CultureInfo" /> to be used by the <see cref="IBinding.Converter" />.</value>
    public System.Globalization.CultureInfo Culture
    {
      set;
      private get;
    }
    /// <summary>
    /// Assigns the <paramref name="value" /> to the associated variable hosted by the target repository.
    /// </summary>
    /// <param name="value">The value.</param>
    public virtual void Assign2Repository(object value)
    {
      if (Converter == null)
        m_Assign((type)value);
      else
        m_Assign((type)Converter.Convert(value, TargetType, Parameter, Culture));
    }
    #endregion

    #region private
    private Action<type> m_Assign;
    #endregion

  }

}
