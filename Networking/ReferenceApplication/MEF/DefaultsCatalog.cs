
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace UAOOI.Networking.ReferenceApplication.MEF
{
  ///<summary>
  /// A very simple custom <see href="ComposablePartCatalog" /> that takes an enumeration
  /// of parts and returns them when requested.
  ///</summary>
  internal class DefaultsCatalog : ComposablePartCatalog
  {

    ///<summary>
    /// Creates a DefaultsCatalog that will return the provided parts when requested.
    ///</summary>
    ///<param name="parts">Parts to add to the catalog</param>
    ///<exception cref="ArgumentNullException">Thrown if the parts parameter is null.</exception>
    public DefaultsCatalog(IEnumerable<ComposablePartDefinition> parts)
    {
      if (parts == null)
        throw new ArgumentNullException(nameof(parts));
      this.m_Parts = parts;
    }
    /// <summary>
    /// Gets the parts contained in the catalog.
    /// </summary>
    public override IQueryable<ComposablePartDefinition> Parts
    {
      get { return m_Parts.AsQueryable(); }
    }

    private readonly IEnumerable<ComposablePartDefinition> m_Parts;
  }
}
