//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System.Collections.Generic;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  /// <summary>
  /// Interface ISemanticDataSetSource - captures a set of semantic data source. Each data entity is represented as the <see cref="BaseVariableState"></see> instance.
  /// </summary>
  public interface ISemanticDataSetSource
  {
    /// <summary>
    /// Gets the name of the semantic data set.
    /// </summary>
    /// <value>The name of the semantic data set.</value>
    string SemanticDataSetName { get; }
    /// <summary>
    /// Gets the <see cref="BaseVariableState"/> with the specified key.
    /// </summary>
    /// <param name="key">The browse path of the <see cref="BaseVariableState"/> to get.</param>
    /// <returns>
    /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="System.Collections.Generic.KeyNotFoundException"/>.
    /// </returns>
    /// <exception cref="System.ArgumentNullException"><paramref name="key"/> is null.</exception>
    /// <exception cref="System.Collections.Generic.KeyNotFoundException">The <paramref name="key"/> does not exist in the collection.</exception>
    IVariable this[string[] key] { get; }
    /// <summary>
    /// Gets the keys in an instance of the <see cref="IEnumerable{string}"/>.
    /// </summary>
    /// <value>An instance of the <see cref="IEnumerable{string}"/> containing the keys.
    IEnumerable<string> Keys { get; }
    /// <summary>
    /// Gets the count of items in this set.
    /// </summary>
    /// <value>The number of items in the set.</value>
    int Count { get; }
    /// <summary>
    /// Determines whether this set contains key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns><c>true</c> if this set contains key; otherwise, <c>false</c>.</returns>
    bool ContainsKey(string key);
  }
}
