//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.Networking.Simulator.Boiler.AddressSpace
{
  /// <summary>
  /// Interface ISemanticDataSource - exposes the enumerator of the application semantic data source. Each data entity as the data source is represented as the <see cref="IVariable"</see> instance and identified using two strings.
  /// </summary>
  /// <seealso cref="System.IDisposable" />
  public interface ISemanticDataSource : IDisposable
  {
    /// <summary>
    /// Gets the semantic data set sources.
    /// </summary>
    /// <param name="registerSemanticData">The delegates used to inverse of the control and register the data source by the the caller.</param>
    void GetSemanticDataSources(RegisterSemanticData registerSemanticData);

  }
  public delegate void RegisterSemanticData(string semanticDataSetRootBrowsePath, string variableRealtiveBrowsePath, IVariable variable);

}