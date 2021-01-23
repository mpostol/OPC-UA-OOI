//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
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
    /// <param name="traceEvent">The trace event delegate capturing the action used to trace all event encountered during the model generation.</param>
    /// <returns>An instance of the <see cref="IModelFactory"/> to be used to generate the OPC UA Information Model captured as the <see cref="XML.ModelDesign"/>.</returns>
    /// <exception cref="ArgumentNullException">outputFilePtah</exception>
    public IModelFactory GetFactory(Action<TraceMessage> traceEvent)
    {
      if (traceEvent == null)
        traceEvent = x => { };
      m_Model = new ModelFactory(traceEvent);
      m_traceEvent = traceEvent;
      return m_Model;
    }
    /// <summary>
    ///  Serializes the already generated model using the interface <see cref="IModelFactory"/> and writes the XML document to a file.
    /// </summary>
    /// <param name="outputFilePtah">A relative or absolute path for the file containing the serialized object.</param>
    public void ExportToXMLFile(string outputFilePtah)
    {
      ExportToXMLFile(string.Empty);
    }
    /// <summary>
    ///  Serializes the already generated model using the interface <see cref="IModelFactory"/> and writes the XML document to a file.
    /// </summary>
    /// <param name="outputFilePtah">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="stylesheetName">Name of the stylesheet document.</param>
    public void ExportToXMLFile(string outputFilePtah, string stylesheetName)
    {
      if (m_Model == null)
        throw new ArgumentNullException("UAModelDesign", "The model must be generated first.");
      if (String.IsNullOrEmpty(outputFilePtah))
        throw new ArgumentNullException(nameof(outputFilePtah), $"{nameof(outputFilePtah)} must be a valid file path.");
      XML.ModelDesign _model = m_Model.Export();
      XML.XmlFile.WriteXmlFile<XML.ModelDesign>(_model, outputFilePtah, FileMode.Create, stylesheetName);
      m_traceEvent(TraceMessage.DiagnosticTraceMessage($"The ModelDesign XML has been saved to file {outputFilePtah} and decorated with the stylesheet {stylesheetName}"));
    }
    /// <summary>
    /// Convert the UA Information Model to graph of objects
    /// </summary>
    /// <returns>Returns an instance of the type <see cref="XML.ModelDesign"/>.</returns>
    public XML.ModelDesign ExportToObject()
    {
      m_traceEvent(TraceMessage.DiagnosticTraceMessage($"The ModelDesign a graph of objects is exporting"));
      return m_Model.Export();
    }
    private ModelFactory m_Model = null;
    private Action<TraceMessage> m_traceEvent;
  }
}
