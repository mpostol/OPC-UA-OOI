//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.ComponentModel;

namespace UAOOI.Configuration.Networking
{
  /// <summary>
  /// Class CommonDefinitions - contains common definition.
  /// </summary>
  internal static class CommonDefinitions
  {

    /// <summary>
    /// The namespace used for serialization of the configuration.
    /// </summary>
    internal const string Namespace = "http://commsvr.com/UAOOI/SemanticData/UANetworking/Configuration/Serialization.xsd";
    internal const string Serializer = "XML";
    /// <summary>
    /// Extension method that sets a new value in a variable and then executes the event handler if the new value
    /// differs from the old one. Used to easily implement <see cref="INotifyPropertyChanged" />.
    /// </summary>
    /// <typeparam name="T">The type of values being handled (usually the type of the property).</typeparam>
    /// <param name="handler">The event handler to execute in the event of actual value change.</param>
    /// <param name="newValue">The new value to set.</param>
    /// <param name="oldValue">The old value to replace (and the value holder).</param>
    /// <param name="update">The delegate used to update the property.</param>
    /// <param name="propertyName">The property's name.</param>
    /// <param name="sender">The object to be appointed as the executioner of the handler.</param>
    /// <returns>A boolean value that indicates if the new value was truly different from the old value according to <code>object.Equals()</code>.</returns>
    internal static bool RaiseHandler<T>(this PropertyChangedEventHandler handler, T newValue, T oldValue, Action<T> update, string propertyName, object sender)
    {
      bool changed = !Object.Equals(oldValue, newValue);
      if (changed)
      {
        //Save the new value. 
        update(newValue);
        //Raise the event 
        handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
      }
      //Signal what happened. 
      return changed;
    }

  }
}
