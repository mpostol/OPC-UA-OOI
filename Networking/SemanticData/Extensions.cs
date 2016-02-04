
using System;
using System.ComponentModel;

namespace UAOOI.Networking.SemanticData
{
  /// <summary>
  /// Class Extensions - provides a set of static helper methods for this library.
  /// </summary>
  internal static class Extensions
  {

    /// <summary>
    /// Extension method that sets a new value in a variable and then executes the event handler if the new value
    /// differs from the old one.  Used to easily implement <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    /// <typeparam name="T">The type of values being handled by the property.</typeparam>
    /// <param name="handler">The event handler to execute in the event of actual value change.</param>
    /// <param name="newValue">The new value to set.</param>
    /// <param name="oldValue">The old value to replace (and the value holder).</param>
    /// <param name="propertyName">The property's name as required by <see cref="PropertyChangedEventArgs"/>.</param>
    /// <param name="sender">The object to be appointed as the executioner of the handler.</param>
    /// <returns>A boolean value that indicates if the new value was truly different from the old value according to <see cref="Object.Equals(object, object)"/>.</returns>
    public static bool RaiseHandler<T>(this PropertyChangedEventHandler handler, T newValue, ref T oldValue, string propertyName, object sender)
    {
      bool changed = !Object.Equals(oldValue, newValue);
      if (changed)
      {
        //Save the new value. 
        oldValue = newValue;
        //Raise the event 
        if (handler != null)
          handler(sender, new PropertyChangedEventArgs(propertyName));
      }
      //Signal what happened. 
      return changed;
    }
    /// <summary>
    /// Increment the <see cref="ushort"/> with the roll over.
    /// </summary>
    /// <param name="value">The value to be incremented.</param>
    /// <returns>The incremented value.</returns>
    internal static ushort IncRollOver(this ushort value)
    {
      if (value == ushort.MaxValue)
        return 0;
      else
        return ++value;
    }
  }

}
