//_______________________________________________________________
//  Title   : SemanticsDataIndex
//  System  : Microsoft VisualStudio 2015 / C#
//  $LastChangedDate: 2016-07-06 23:00:19 +0200 (Śr, 06 lip 2016) $
//  $Rev: 12264 $
//  $LastChangedBy: mpostol $
//  $URL: svn://svnserver.hq.cas.com.pl/VS/trunk/CommServer.UA.OOI/OOI.ConfigurationEditor/DomainsModel/SemanticsDataIndex.cs $
//  $Id: SemanticsDataIndex.cs 12264 2016-07-06 21:00:19Z mpostol $
//
//  Copyright (C) 2016, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//_______________________________________________________________

namespace UAOOI.DataDiscovery.DiscoveryServices.Models
{

  /// <summary>
  /// Class SemanticsDataId - identifier unique in context of the selected domain
  /// </summary>
  /// <remarks>
  /// Each Semantic data belongs to the only one domain and must have symbolic name unique in context of the domain.
  /// Index is used to replace the symbolic name with the purpose of optimization of the data transfer.
  /// </remarks>
  public partial class SemanticsDataIndex { }

}
