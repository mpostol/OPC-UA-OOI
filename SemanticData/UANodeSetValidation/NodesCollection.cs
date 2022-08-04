//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System.Collections.Generic;
using System.Linq;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Class NodesCollection - manages a collection of nodes.
  /// Implements the <see cref="Dictionary{String, IUANodeBase}" />
  /// </summary>
  /// <seealso cref="Dictionary{String, IUANodeBase}" />
  internal class NodesCollection : Dictionary<string, IUANodeBase>
  {
    /// <summary>
    /// Adds the {<paramref name="item"/> to the collection. Replace existing if required.
    /// </summary>
    /// <param name="item">The item to be added to the collection.</param>
    /// <param name="replaceIfExist">if set to <c>true</c> replace the existing entity in the collection</param>
    internal void AddOrReplace(IUANodeBase item, bool replaceIfExist)
    {
      string _key = item.UANode.BrowseName.Name;
      if (!this.ContainsKey(_key))
      {
        this.Add(_key, item); //add derived item
        return;
      }
      if (replaceIfExist)
        this[_key] = item; //replace by current item that overrides the base one
    }

    /// <summary>
    /// Converts to list.
    /// </summary>
    /// <returns>returns an instance of <see cref="List{IUANodeBase}"/></returns>
    internal List<IUANodeBase> ToList()
    {
      return this.Values.ToList<IUANodeBase>();
    }
  }
}