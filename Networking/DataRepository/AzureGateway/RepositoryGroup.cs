//____________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/TP
//____________________________________________________________________________

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  /// <summary>
  /// Class RepositoryGroup - it is a process state replica implementing Data Transfer Object.
  /// Implements the <see cref="IDictionary{String, Object}" />
  /// </summary>
  /// <seealso cref="IDictionary{String, Object}" />
  internal class RepositoryGroup : IDictionary<string, Object>
  {
    #region API

    /// <summary>
    /// Adds the property to the Dada Transfer Object.
    /// </summary>
    /// <typeparam name="type">The type of the type.</typeparam>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>Action&lt;type&gt;.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Duplicated property name: {propertyName}</exception>
    internal Action<type> AddProperty<type>(string propertyName)
    {
      if (_processReplica.ContainsKey(propertyName))
        throw new ArgumentOutOfRangeException($"Duplicated property name: {propertyName}");
      _processReplica.Add(propertyName, default(type));
      //object _value = _processReplica[propertyName];
      return x => Updater<type>(propertyName, x);
    }

    #endregion API

    #region IDictionary<string, Object>

    /// <summary>
    /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
    /// </summary>
    /// <value>The keys.</value>
    public ICollection<string> Keys => _processReplica.Keys;

    /// <summary>
    /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"></see> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
    /// </summary>
    /// <value>The values.</value>
    /// <exception cref="System.NotImplementedException"></exception>
    public ICollection<object> Values => throw new NotImplementedException();

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
    /// </summary>
    /// <value>The count.</value>
    public int Count => _processReplica.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
    /// </summary>
    /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
    public bool IsReadOnly => true;

    /// <summary>
    /// Gets or sets the <see cref="System.Object"/> with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>System.Object.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public object this[string key] { get => _processReplica[key]; set => throw new NotImplementedException(); }

    /// <summary>
    /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
    /// </summary>
    /// <param name="key">The object to use as the key of the element to add.</param>
    /// <param name="value">The object to use as the value of the element to add.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Add(string key, object value)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.</param>
    /// <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the key; otherwise, false.</returns>
    public bool ContainsKey(string key)
    {
      return _processReplica.ContainsKey(key);
    }

    /// <summary>
    /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2"></see>.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key">key</paramref> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"></see>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool Remove(string key)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose value to get.</param>
    /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns>true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"></see> contains an element with the specified key; otherwise, false.</returns>
    public bool TryGetValue(string key, out object value)
    {
      return _processReplica.TryGetValue(key, out value);
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Add(KeyValuePair<string, object> item)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Clear()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
    /// <returns>true if <paramref name="item">item</paramref> is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.</returns>
    public bool Contains(KeyValuePair<string, object> item)
    {
      return ((IDictionary<string, object>)_processReplica).Contains(item);
    }

    /// <summary>
    /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      ((IDictionary<string, object>)_processReplica).CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
    /// <returns>true if <paramref name="item">item</paramref> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if <paramref name="item">item</paramref> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public bool Remove(KeyValuePair<string, object> item)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return _processReplica.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _processReplica.GetEnumerator();
    }

    #endregion IDictionary<string, Object>

    #region object

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }

    #endregion object

    #region private

    private readonly Dictionary<string, object> _processReplica = new Dictionary<string, object>();

    private object GetPropertyValue(string propertyName)
    {
      object result = null;
      if (_processReplica.ContainsKey(propertyName))
        result = _processReplica[propertyName];
      return result;
    }

    private void Updater<type>(string propertyName, type value)
    {
      _processReplica[propertyName] = value;
    }

    #endregion private
  }
}