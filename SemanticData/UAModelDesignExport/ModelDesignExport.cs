//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Diagnostics;
using System.IO;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Common.Infrastructure.Serializers;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.Diagnostic;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Class ModelDesignExport - captures functionality supporting export functionality of the OPC UA Information Model represented by an XML file compliant with UA Model Design.
  /// </summary>
  public abstract class ModelDesignExportAPI
  {
    #region factory

    /// <summary>
    /// Gets the model design export.
    /// </summary>
    /// <returns>An instance capturing model design export implemented using the <see cref="IModelDesignExport"/> interface.</returns>
    public static IModelDesignExport GetModelDesignExport()
    {
      return new ModelDesignExport();
    }

    internal static IModelDesignExport GetModelDesignExport(ITraceSource traceSource)
    {
      return new ModelDesignExport(traceSource);
    }

    #endregion factory

    private class ModelDesignExport : IModelDesignExport
    {
      #region IModelDesignExport

      /// <summary>
      /// Gets the factory.
      /// </summary>
      /// <returns>An instance of the <see cref="IModelFactory"/> to be used to generate the OPC UA Information Model captured as the <see cref="XML.ModelDesign"/>.</returns>
      public IModelFactory GetFactory()
      {
        m_Model = new ModelFactory(m_traceEvent.WriteTraceMessage);
        return m_Model;
      }

      /// <summary>
      ///  Serializes the already generated model using the interface <see cref="IModelFactory"/> and writes the XML document to a file.
      /// </summary>
      /// <param name="outputFilePtah">A relative or absolute path for the file containing the serialized object.</param>
      public void ExportToXMLFile(string outputFilePtah)
      {
        ExportToXMLFile(outputFilePtah, string.Empty);
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
        if (string.IsNullOrEmpty(outputFilePtah))
          throw new ArgumentNullException(nameof(outputFilePtah), $"{nameof(outputFilePtah)} must be a valid file path.");
        XML.ModelDesign _model = m_Model.Export();
        XmlFile.WriteXmlFile<XML.ModelDesign>(_model, outputFilePtah, FileMode.Create, stylesheetName);
        m_traceEvent.TraceData(TraceEventType.Information, 279330276, $"The ModelDesign XML has been saved to file {outputFilePtah} and decorated with the stylesheet {stylesheetName}");
      }

      /// <summary>
      /// Convert the UA Information Model to graph of objects
      /// </summary>
      /// <returns>Returns an instance of the type <see cref="XML.ModelDesign"/>.</returns>
      public XML.ModelDesign ExportToObject()
      {
        m_traceEvent.TraceData(TraceEventType.Information, 52892026, $"The ModelDesign a graph of objects is exporting");
        return m_Model.Export();
      }

      #endregion IModelDesignExport

      #region private

      private ModelFactory m_Model = null;
      private readonly AssemblyTraceSource m_traceEvent;

      internal ModelDesignExport(ITraceSource traceEvent)
      {
        m_traceEvent = new AssemblyTraceSource(traceEvent);
      }

      internal ModelDesignExport()
      {
        m_traceEvent = new AssemblyTraceSource();
      }

      #endregion private
    }
  }
}