
namespace UAOOI.SemanticData.DataManagement
{
  public abstract class IntegrationServicesBase<IntegrationServicesType> : IIntegrationServices
    where IntegrationServicesType : class, new()
  {

    public static IntegrationServicesType GetIntegrationServices()
    {
      if (m_Services == null)
        m_Services = new IntegrationServicesType();
      return m_Services;
    }

    /// <summary>
    /// Adds the association.
    /// </summary>
    /// <param name="association">The association to be added to collection of association managed by the interface instance.</param>
    /// <param name="name">The name of the association.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void AddAssociation(IAssociation association, string name)
    {
      throw new System.NotImplementedException();
    }
    #region IIntegrationServices
    /// <summary>
    /// Removes the association. This method is used to remove a <see cref="IAssociation" /> object from from the collection of managed association.
    /// A successful removal of the object removes all child Objects and terminates the related communication.
    /// </summary>
    /// <param name="association">The association.</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public abstract void RemoveAssociation(IAssociation association);
    /// <summary>
    /// Remove all association and shutdown all services gracefully.
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    public abstract void Shutdown();
    #endregion

    //private
    private static IntegrationServicesType m_Services = null;

  }
}
