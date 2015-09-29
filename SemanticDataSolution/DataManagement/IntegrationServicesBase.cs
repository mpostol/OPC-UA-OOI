
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UAOOI.SemanticData.DataManagement
{
  public class IntegrationServices : IIntegrationServices, IDisposable
  {

    #region creator
    public IntegrationServices() { }
    #endregion

    #region IIntegrationServices
    /// <summary>
    /// Adds the association.
    /// </summary>
    /// <param name="association">The association to be added to collection of associations managed by the interface instance.</param>
    /// <param name="name">The name of the association.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void AddAssociation(IAssociation association, string name)
    {
      throw new System.NotImplementedException();
    }
    /// <summary>
    /// Removes the association. This method is used to remove a <see cref="IAssociation" /> object from from the collection of managed association.
    /// A successful removal of the object removes all child Objects and terminates the related communication.
    /// </summary>
    /// <param name="association">The association.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void RemoveAssociation(IAssociation association)
    {
      if (association == null)
        throw new NullReferenceException("association");
      int _idx = m_IAssociation.IndexOf(association);
      if (_idx == -1)
        throw new ArgumentOutOfRangeException("association");
      //TODO m_IAssociation[_idx].
      Debug.Assert(m_IAssociation.Remove(association));
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// Remove all association and shutdown all services gracefully.
    /// </summary>
    public void Dispose()
    {
      throw new NotImplementedException();
    }
    #endregion

    private List<IAssociation> m_IAssociation = new List<IAssociation>();

  }
}
