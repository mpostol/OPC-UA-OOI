//_______________________________________________________________
//  Title   : Name of Application
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate: 2016-07-12 10:03:06 +0200 (Wt, 12 lip 2016) $
//  $Rev: 12270 $
//  $LastChangedBy: mpostol $
//  $URL: svn://svnserver.hq.cas.com.pl/VS/trunk/CommServer.UA.OOI/OOI.ConfigurationEditor/DomainsModel/DomainModel.xsd.cs $
//  $Id: DomainModel.xsd.cs 12270 2016-07-12 08:03:06Z mpostol $
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

using System;
using System.Xml;
using System.Xml.Serialization;

namespace UAOOI.DataDiscovery.DiscoveryServices.Models
{
  /// <summary>
  /// Class DomainModel - domain description holder. 
  /// </summary>
  /// <remarks>
  /// Domain is a collection of data over which an owner has control. It may be used to describe:
  /// * a collection of package addresses used to push the message to the receiver.
  /// * a collection of data used to provide data semantic unique identifier and support subscription to receive copies of the data as the message payload based on the data semantics.
  /// </remarks>
  public partial class DomainModel
  {

    #region API
    /// <summary>
    /// Gets or sets the URI of the domain.
    /// </summary>
    /// <value>The URI.</value>
    [XmlIgnore]
    public Uri DomainModelUri
    {
      get
      {
        return new Uri(DomainModelUriString);
      }
      set
      {
        DomainModelUriString = value.ToString();
      }
    }
    /// <summary>
    /// Gets or sets the unique name of the domain.
    /// </summary>
    /// <value>The name of the unique.</value>
    [XmlIgnore]
    public Guid DomainModelGuid
    {
      get
      {
        return XmlConvert.ToGuid(DomainModelGuidString);
      }
      set
      {
        DomainModelGuidString = XmlConvert.ToString(value);
      }
    }
    /// <summary>
    /// Gets or sets the universal discovery service locator - this URL (REST call is assigned by the resolver).
    /// </summary>
    /// <value>The universal discovery service locator.</value>
    [XmlIgnore]
    public string UniversalDiscoveryServiceLocator
    {
      get
      {
        return this.universalDiscoveryServiceLocatorField;
      }
      set
      {
        this.universalDiscoveryServiceLocatorField = value;
      }
    }
    #endregion

    #region private
    private string universalDiscoveryServiceLocatorField;
    #endregion

  }
}
