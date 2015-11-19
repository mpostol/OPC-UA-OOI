
using System;
using System.Globalization;
using System.Windows.Data;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.DataRepository
{

  /// <summary>
  /// Interface IBinding is used to update a destination variable by an owner of this object.
  /// It captures an association targeted a variable that is to be updated and all information required to convert the value to be compliant with the target type.
  /// The owner of this object is responsible to provide converter an an instance of <see cref="IValueConverter"/> and required by it information.
  /// </summary>
  public interface IBinding
  {
    /// <summary>
    /// Sets the converter, which is used to provide a way to apply custom logic to a binding.
    /// </summary>
    /// <value>The converter as an instance of the <see cref="IValueConverter"/>.</value>
    IValueConverter Converter { set; }
    /// <summary>
    /// Gets the type of the variable repository.
    /// </summary>
    /// <value>The type of the repository target variable of the binding.</value>
    BuiltInType TargetType { get; }
    /// <summary>
    /// Sets an optional parameter to be used in the converter logic.
    /// </summary>
    /// <value>The parameter to be used by the <see cref="IBinding.Converter"/>.</value>
    object Parameter { get; set; }
    /// <summary>
    /// Sets the culture of the conversion.
    /// </summary>
    /// <value>The culture as an instance of the <see cref="CultureInfo"/> to be used by the <see cref="IBinding.Converter"/>.</value>
    CultureInfo Culture { set; }
    /// <summary>
    /// Marks the process value enabled - signal that the update of the value is expected.
    /// </summary>
    void OnEnabling();
    /// <summary>
    /// Marks the process value disabled - signal that the value will not be updated.
    /// </summary>
    void OnDisabling();
  }

}
