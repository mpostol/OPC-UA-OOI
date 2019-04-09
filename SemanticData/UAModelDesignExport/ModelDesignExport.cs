//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Class ModelDesignExport - captures functionality supporting export functionality of the OPC UA Information Model represented by an xml file compliant with UA Model Design.
  /// </summary>
  public class ModelDesignExport
  {
    /// <summary>
    /// Gets the factory.
    /// </summary>
    /// <param name="outputFilePtah">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="traceEvent">The trace event delegate capturing the action used to trace all event encountered during the model generation.</param>
    /// <returns>An instance of the <see cref="IModelFactory"/> to be used to generate the OPC UA Information Model captured as the <see cref="XML.ModelDesign"/>.</returns>
    /// <exception cref="ArgumentNullException">outputFilePtah</exception>
    public IModelFactory GetFactory(string outputFilePtah, Action<TraceMessage> traceEvent)
    {
      if (String.IsNullOrEmpty(outputFilePtah))
        throw new ArgumentNullException(nameof(outputFilePtah), $"{nameof(outputFilePtah)} must be a valid file path.");
      m_OutputPath = outputFilePtah;
      if (traceEvent == null)
        traceEvent = x => { };
      m_Model = new ModelFactory(traceEvent);
      m_traceEvent = traceEvent;
      return m_Model;
    }
    /// <summary>
    ///  Serializes the already generated model using the interface <see cref="IModelFactory"/> and writes the XML document to a file.
    /// </summary>
    public void ExportToXMLFile()
    {
      ExportToXMLFile(string.Empty);
    }
    /// <summary>
    ///  Serializes the already generated model using the interface <see cref="IModelFactory"/> and writes the XML document to a file.
    /// </summary>
    /// <param name="stylesheetName">Name of the stylesheet document.</param>
    public void ExportToXMLFile(string stylesheetName)
    {
      if (m_Model == null)
        throw new ArgumentNullException("UAModelDesign", "The model must be generated first.");
      XML.ModelDesign _model = m_Model.Export();
      XML.XmlFile.WriteXmlFile<XML.ModelDesign>(_model, m_OutputPath, FileMode.Create, stylesheetName);
      m_traceEvent(TraceMessage.DiagnosticTraceMessage($"The ModelDesign XML has been saved to file {m_OutputPath} and decorated with the stylesheet {stylesheetName}"));

    }
    private ModelFactory m_Model = null;
    private Action<TraceMessage> m_traceEvent;
    private string m_OutputPath = string.Empty;
  }
}
