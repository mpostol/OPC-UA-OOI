//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Configuration.Networking.Serialization;

namespace UAOOI.Configuration.Networking.Upgrade
{
  /// <summary>
  /// Class UpdateConfiguration - provides helper function aimed at configuration management.
  /// </summary>
  public static class ConfigurationManagement
  {
    /// <summary>
    /// Creates the configuration.
    /// </summary>
    /// <param name="dataSourceFields">The data source fields.</param>
    /// <param name="associationName">Name of the association used to select the <see cref="DataSetConfiguration"/> to be modified and added to the output file.</param>
    /// <param name="inFileName">Name of the in file containing the configuration.</param>
    /// <param name="outFileName">Name of the out file containing the configuration.</param>
    /// <param name="traceSource">The trace source.</param>
    public static void ReplaceDataSetFields(FieldMetaData[] dataSourceFields, string associationName, string inFileName, string outFileName, ITraceSource traceSource)
    {
      traceSource.TraceData(TraceEventType.Verbose, 53, $"Entering {nameof(ReplaceDataSetFields)} method.");
      UANetworkingConfiguration<ConfigurationData> _newConfiguration = new UANetworkingConfiguration<ConfigurationData>();
      FileInfo _file2ReadConfiguration = new FileInfo(inFileName);
      _newConfiguration.ReadConfiguration(_file2ReadConfiguration);
      traceSource.TraceData(TraceEventType.Verbose, 53, $"I have read the configuration form the file {_file2ReadConfiguration.FullName}.");
      _newConfiguration.TraceSource = traceSource;
      Dictionary<string, DataSetConfiguration> _dataSets = _newConfiguration.CurrentConfiguration.DataSets.ToDictionary<DataSetConfiguration, string>(_dsc => _dsc.AssociationName);
      traceSource.TraceData(TraceEventType.Verbose, 53, $"Configuration contains {_dataSets.Count} item of type {nameof(DataSetConfiguration)}.");
      _dataSets[associationName].DataSet = dataSourceFields;
      traceSource.TraceData(TraceEventType.Verbose, 53, $"{nameof(DataSetConfiguration.DataSet)} for the association {associationName} has been modified by array of {dataSourceFields.Length} instances.");
      //_newConfiguration.CurrentConfiguration.DataSets = new DataSetConfiguration[] { _dataSets[associationName] };
      FileInfo _file2SaveConfiguration = new FileInfo(outFileName);
      _newConfiguration.SaveConfiguration(_file2SaveConfiguration);
      traceSource.TraceData(TraceEventType.Verbose, 53, $"Configuration has been saved to the file {_file2SaveConfiguration.FullName}.");
    }
    /// <summary>
    /// Adds the data set configuration.
    /// </summary>
    /// <param name="newDataSetConfiguration">The new data set configuration.</param>
    /// <param name="inFileName">Name of the in file.</param>
    /// <param name="outFileName">Name of the out file.</param>
    /// <param name="writerId">The writer identifier.</param>
    /// <param name="traceSource">The trace source.</param>
    public static void AddDataSetConfiguration(this DataSetConfiguration newDataSetConfiguration, Tuple<string, ushort, Guid> writerId, string inFileName, string outFileName, ITraceSource traceSource)
    {
      traceSource.TraceData(TraceEventType.Verbose, 53, $"Entering {nameof(AddDataSetConfiguration)} method.");
      //open source configuration
      UANetworkingConfiguration<ConfigurationData> _newConfiguration = new UANetworkingConfiguration<ConfigurationData>();
      FileInfo _file2ReadConfiguration = new FileInfo(inFileName);
      _newConfiguration.ReadConfiguration(_file2ReadConfiguration);
      traceSource.TraceData(TraceEventType.Verbose, 53, $"I have read the configuration form the file {_file2ReadConfiguration.FullName}.");
      _newConfiguration.TraceSource = traceSource;
      ConfigurationData _currentConfiguration = _newConfiguration.CurrentConfiguration;
      // ddd new DataSetConfiguration
      List<DataSetConfiguration> _dataSets = _currentConfiguration.DataSets.ToList<DataSetConfiguration>();
      traceSource.TraceData(TraceEventType.Verbose, 53, $"Configuration contains {_dataSets.Count} item of type {nameof(DataSetConfiguration)}.");
      _dataSets.Add(newDataSetConfiguration);
      _currentConfiguration.DataSets = _dataSets.ToArray<DataSetConfiguration>();
      traceSource.TraceData(TraceEventType.Verbose, 53, $"New {nameof(DataSetConfiguration)} has been added to the copnfiguration.");
      // add new association for this DataSet in selected MessageWriterConfiguration
      ProducerAssociationConfiguration _newAssociation = new ProducerAssociationConfiguration()
      {
        AssociationName = newDataSetConfiguration.AssociationName,
        DataSetWriterId = writerId.Item2,
        FieldEncoding = FieldEncodingEnum.VariantFieldEncoding,
        PublisherId = writerId.Item3
      };
      MessageWriterConfiguration _selectedWriterConfiguration = _currentConfiguration.MessageHandlers
        .Where<MessageHandlerConfiguration>(_hn => (_hn.Name == writerId.Item1) && (_hn is MessageWriterConfiguration))
        .Cast<MessageWriterConfiguration>()
        .First<MessageWriterConfiguration>();
      List<ProducerAssociationConfiguration> _associationsInWriterConfiguration = _selectedWriterConfiguration.ProducerAssociationConfigurations.ToList<ProducerAssociationConfiguration>();
      _associationsInWriterConfiguration.Add(_newAssociation);
      _selectedWriterConfiguration.ProducerAssociationConfigurations = _associationsInWriterConfiguration.ToArray();
      traceSource.TraceData(TraceEventType.Verbose, 53, $"New {nameof (ProducerAssociationConfiguration)}  has been added to the {nameof(MessageWriterConfiguration)}[{writerId.Item1}].");
      //save the output file
      FileInfo _file2SaveConfiguration = new FileInfo(outFileName);
      _newConfiguration.SaveConfiguration(_file2SaveConfiguration);
      traceSource.TraceData(TraceEventType.Verbose, 53, $"Configuration has been saved to the file {_file2SaveConfiguration.FullName}.");

    }

  }
}
