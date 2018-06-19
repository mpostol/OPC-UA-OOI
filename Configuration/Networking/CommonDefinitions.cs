
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo(
  "UAOOI.Configuration.Networking.UnitTest, PublicKey=" +
    "00240000048000009400000006020000002400005253413100040000010001005b97a0972ff6b13a" +
    "8a9ff9c09503aea0e5e2fe29cb2275a0c0942182f4c3431814b6bc9a556d9fe0d7e7823439c1ba28" +
    "521f6318e4c936c4461604ef668e9686c2021571b093e1bfba071b373bc56a07a3afdc120c5313d3" +
    "9a935cda64b759f857ebb3db483641444a5347e1564f8ba6d4fad2f968d3caf9991a4fa6aa019ebe"
)]
#endif

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
