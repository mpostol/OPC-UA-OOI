//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UAModelDesignExport.XML;

namespace UAOOI.SemanticData.UAModelDesignExport
{
  /// <summary>
  /// Interface IModelDesignExport - abstract API of the UAModelDesignExport.
  /// </summary>
  public interface IModelDesignExport
  {
    /// <summary>
    /// Gets the factory.
    /// </summary>
    /// <returns>An instance of the <see cref="IModelFactory"/> to be used to generate the OPC UA Information Model captured as the <see cref="ModelDesign"/>.</returns>
    IModelFactory GetFactory();

    /// <summary>
    ///  Serializes the already generated model using the interface <see cref="IModelFactory"/> and writes the XML document to a file.
    /// </summary>
    /// <param name="outputFilePtah">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="stylesheetName">Name of the stylesheet document.</param>
    void ExportToXMLFile(string outputFilePtah, string stylesheetName);

    /// <summary>
    ///  Serializes the already generated model using the interface <see cref="IModelFactory"/> and writes the XML document to a file.
    /// </summary>
    /// <param name="outputFilePtah">A relative or absolute path for the file containing the serialized object.</param>
    void ExportToXMLFile(string outputFilePtah);

    /// <summary>
    /// Convert the UA Information Model to graph of objects
    /// </summary>
    /// <returns>Returns an instance of the type <see cref="ModelDesign"/>.</returns>
    ModelDesign ExportToObject();
  }
}