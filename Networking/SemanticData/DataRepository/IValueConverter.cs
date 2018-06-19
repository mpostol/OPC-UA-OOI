
using System.Globalization;
using System;

namespace UAOOI.Networking.SemanticData.DataRepository
{

  /// <summary>
  /// Interface IValueConverter: provides a way to apply custom logic to a binding.
  /// </summary>
  /// <typeparam name="targetType">The type of the binding target property.</typeparam>
  /// <typeparam name="sourceType">The type of the source type.</typeparam>
  /// <typeparam name="parameterType">The type of the parameter type.</typeparam>
  /// <remarks>
  /// If you want to associate a value converter with a binding, create a class that implements the <c>IValueConverter</c> interface and then implement the Convert methods. 
  /// Converters can change data from one type to another, translate data based on cultural information, or modify other aspects of the presentation. 
  /// Value converters are culture aware. Both the <c>Convert</c> methods have a culture parameter that indicates the cultural information. 
  /// If cultural information is irrelevant to the conversion, then you can ignore that parameter in your custom converter.
  /// The <c>Convert</c> methods also have a parameter called <c>parameter</c> so that you can use the same instance of the converter with different parameters. 
  /// For example, you can write a formatting converter that produces different formats of data based on the input parameter that you use. 
  /// </remarks>
  public interface IValueConverter
  {

    /// <summary>
    /// Converts the specified value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="fallBack">The value to use when the binding is unable to return a value.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value.</returns>
    /// <remarks>
    /// The data binding engine calls this method when it propagates a value from the binding source to the binding target.
    /// The data binding engine does not catch exceptions that are thrown by a user-supplied converter. Any exception that is thrown by the method, or any uncaught exceptions
    /// that are thrown by methods that the <c>Convert</c> method calls, are treated as run-time errors.
    /// </remarks>
    object Convert(object value, Type targetType, object fallBack, object parameter, CultureInfo culture);

    ///// <summary>
    ///// Converts the specified value.
    ///// </summary>
    ///// <param name="value">The value produced by the binding target.</param>
    ///// <param name="fallBack">The value to use when the binding is unable to return a value.</param>
    ///// <param name="parameter">The converter parameter to use.</param>
    ///// <param name="culture">The culture to use in the converter.</param>
    ///// <returns>A converted value.</returns>
    ///// <remarks>
    ///// The data binding engine calls this method when it propagates a value from the binding target to the binding source.
    ///// The implementation of this method must be the inverse of the overloaded <c>Convert</c> method.
    ///// The data binding engine does not catch exceptions that are thrown by a user supplied converter. Any exception that is thrown by the method, or any uncaught exceptions that are 
    ///// thrown by methods that the method calls, are treated as run-time errors. 
    //    sourceType Convert(targetType value, targetType fallBack, parameterType parameter, CultureInfo culture);

  }
}
