
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UAOOI.Networking.ReferenceApplication.Core.MvvmLight
{
  /// <summary>
  /// A base class for objects of which the properties must be observable.
  /// </summary>
  public class ObservableObject : INotifyPropertyChanged
  {
    #region INotifyPropertyChanged
    /// <summary>
    /// Occurs after a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    /// <summary>
    /// Occurs before a property value changes.
    /// </summary>
    public event PropertyChangingEventHandler PropertyChanging;

    /// <summary>
    /// Verifies that a property name exists in this ViewModel. This method
    /// can be called before the property is used, for instance before
    /// calling RaisePropertyChanged. It avoids errors when a property name
    /// is changed but some places are missed.
    /// </summary>
    /// <remarks>This method is only active in DEBUG mode.</remarks>
    /// <param name="propertyName">The name of the property that will be checked.</param>

    [Conditional("DEBUG")]
    public void VerifyPropertyName(string propertyName)
    {
      Type _Type = GetType();
      if (!string.IsNullOrEmpty(propertyName) && _Type.GetProperty(propertyName) == null)
      {
        ICustomTypeDescriptor _descriptor = this as ICustomTypeDescriptor;
        if (_descriptor != null)
        {
          if (_descriptor.GetProperties().Cast<PropertyDescriptor>().Any(property => property.Name == propertyName))
            return;
        }
        throw new ArgumentException("Property not found", propertyName);
      }
    }
    /// <summary>
    /// Raises the PropertyChanging event if needed.
    /// </summary>
    /// <remarks>If the propertyName parameter
    /// does not correspond to an existing property on the current class, an
    /// exception is thrown in DEBUG configuration only.</remarks>
    /// <param name="propertyName">The name of the property that
    /// changed.</param>
    public virtual void RaisePropertyChanging(string propertyName)
    {
      VerifyPropertyName(propertyName);
      PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    /// <summary>
    /// Raises the PropertyChanged event if needed.
    /// </summary>
    /// <remarks>If the propertyName parameter
    /// does not correspond to an existing property on the current class, an
    /// exception is thrown in DEBUG configuration only.</remarks>
    /// <param name="propertyName">The name of the property that
    /// changed.</param>
    public virtual void RaisePropertyChanged(string propertyName)
    {
      VerifyPropertyName(propertyName);
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    /// <summary>
    /// Raises the PropertyChanging event if needed.
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changes.</typeparam>
    /// <param name="propertyExpression">An expression identifying the property
    /// that changes.</param>
    public virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
    {
      PropertyChangingEventHandler handler = PropertyChanging;
      if (handler != null)
      {
        string propertyName = GetPropertyName(propertyExpression);
        handler(this, new PropertyChangingEventArgs(propertyName));
      }
    }
    /// <summary>
    /// Raises the PropertyChanged event if needed.
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyExpression">An expression identifying the property that changed.</param>
    public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler == null)
        return;
      string propertyName = GetPropertyName(propertyExpression);
      if (!string.IsNullOrEmpty(propertyName))
        RaisePropertyChanged(propertyName);
    }
    /// <summary>
    /// Raises the PropertyChanged event if needed, and broadcasts a
    /// PropertyChangedMessage using the Messenger instance (or the
    /// static default instance if no Messenger instance is available).
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyName">The name of the property that
    /// changed.</param>
    /// <param name="oldValue">The property's value before the change
    /// occurred.</param>
    /// <param name="newValue">The property's value after the change
    /// occurred.</param>
    public virtual void RaisePropertyChanged<T>([CallerMemberName] string propertyName = null, T oldValue = default(T), T newValue = default(T))
    {
      if (string.IsNullOrEmpty(propertyName))
        throw new ArgumentException("This method cannot be called with an empty string", "propertyName");
      RaisePropertyChanged(propertyName);
    }
    /// <summary>
    /// Raises the PropertyChanged event if needed, and broadcasts a
    /// PropertyChangedMessage using the Messenger instance (or the
    /// static default instance if no Messenger instance is available).
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyExpression">An expression identifying the property
    /// that changed.</param>
    /// <param name="oldValue">The property's value before the change
    /// occurred.</param>
    /// <param name="newValue">The property's value after the change
    /// occurred.</param>
    public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue)
    {
      RaisePropertyChanged(propertyExpression);
    }
    /// <summary>
    /// Assigns a new value to the property. Then, raises the
    /// PropertyChanged event if needed, and broadcasts a
    /// PropertyChangedMessage using the Messenger instance (or the
    /// static default instance if no Messenger instance is available). 
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyExpression">An expression identifying the property
    /// that changed.</param>
    /// <param name="field">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change
    /// occurred.</param>
    protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
    {
      if (EqualityComparer<T>.Default.Equals(field, newValue))
        return false;
      RaisePropertyChanging(propertyExpression);
      T oldValue = field;
      field = newValue;
      RaisePropertyChanged(propertyExpression, oldValue, field);
      return true;
    }
    /// <summary>
    /// Assigns a new value to the property. Then, raises the
    /// PropertyChanged event if needed, and broadcasts a
    /// PropertyChangedMessage using the Messenger instance (or the
    /// static default instance if no Messenger instance is available). 
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyName">The name of the property that
    /// changed.</param>
    /// <param name="field">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change
    /// occurred.</param>
    protected bool Set<T>(string propertyName, ref T field, T newValue = default(T))
    {
      if (EqualityComparer<T>.Default.Equals(field, newValue))
        return false;
      RaisePropertyChanging(propertyName);
      var oldValue = field;
      field = newValue;
      RaisePropertyChanged(propertyName, oldValue, field);
      return true;
    }
    /// <summary>
    /// Extracts the name of a property from an expression.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="propertyExpression">An expression returning the property's name.</param>
    /// <returns>The name of the property returned by the expression.</returns>
    /// <exception cref="ArgumentNullException">If the expression is null.</exception>
    /// <exception cref="ArgumentException">If the expression does not represent a property.</exception>
    protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
      if (propertyExpression == null)
        throw new ArgumentNullException("propertyExpression");
      MemberExpression body = propertyExpression.Body as MemberExpression;
      if (body == null)
        throw new ArgumentException("Invalid argument", "propertyExpression");
      PropertyInfo property = body.Member as PropertyInfo;
      if (property == null)
        throw new ArgumentException("Argument is not a property", "propertyExpression");
      return property.Name;
    }

  }
}
