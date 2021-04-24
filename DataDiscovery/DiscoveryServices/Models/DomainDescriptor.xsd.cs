//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace UAOOI.DataDiscovery.DiscoveryServices.Models
{
  public partial class DomainDescriptor
  {
    /// <summary>
    /// Resolves the URL of the service providing resources describing the data domain using path template and the <paramref name="modelUri"/>.
    /// </summary>
    /// <param name="modelUri">The model URI.</param>
    /// <returns>An instance of <see cref="Uri"/> encapsulating URL of the service responsible to return expected record. It uses regular expression to replace
    /// the key words <c>#authority#</c> and <c>#path#</c> in the <see cref="DomainDescriptor.UrlPattern"/> by appropriate parts of the
    /// <paramref name="modelUri"/>. It is next hope in the resolving process.</returns>
    internal Uri ResolveUri(Uri modelUri)
    {
      Regex _regex = new Regex("#authority#");
      string _resolution = _regex.Replace(UrlPattern, modelUri.Authority);
      string _path = string.Join(@"", modelUri.Segments.Skip<string>(1).ToArray<string>());
      _regex = new Regex("#path#");
      _resolution = _regex.Replace(_resolution, _path);
      return new Uri(_resolution);
    }
  }
}