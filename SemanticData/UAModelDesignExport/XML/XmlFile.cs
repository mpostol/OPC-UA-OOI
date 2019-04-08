//____________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/TP
//____________________________________________________________________________

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace UAOOI.SemanticData.UAModelDesignExport.XML
{
  /// <summary>
  /// Provides static methods for serialization objects into XML documents and writing the XML document to a file.
  /// </summary>
  internal static class XmlFile
  {

    #region public
    /// <summary>
    /// Serializes the specified <paramref name="dataObject"/> and writes the XML document to a file.
    /// </summary>
    /// <typeparam name="type">The type of the root object to be serialized and saved in the file.</typeparam>
    /// <param name="dataObject">The object containing working data to be serialized and saved in the file.</param>
    /// <param name="path">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="mode">Specifies how the operating system should open a file <see cref="FileMode"/>.</param>
    /// <param name="stylesheetName">Name of the stylesheet document.</param>
    /// <exception cref="System.ArgumentNullException">
    /// path
    /// or
    /// dataObject
    /// or
    /// stylesheetName
    /// </exception>
    public static void WriteXmlFile<type>(type dataObject, string path, FileMode mode, string stylesheetName)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException(nameof(path));
      if (dataObject == null)
        throw new ArgumentNullException(nameof(dataObject));
      XmlSerializer _xmlSerializer = new XmlSerializer(typeof(type));
      XmlWriterSettings _setting = new XmlWriterSettings()
      {
        Indent = true,
        IndentChars = "  ",
        NewLineChars = "\r\n"
      };
      using (FileStream _docStream = new FileStream(path, mode, FileAccess.Write))
      {
        XmlWriter _writer = XmlWriter.Create(_docStream, _setting);
        if (!string.IsNullOrEmpty(stylesheetName))
          _writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" " + string.Format("href=\"{0}\"", stylesheetName));
        _xmlSerializer.Serialize(_writer, dataObject);
      }
    }
    /// <summary>
    /// Serializes the specified <paramref name="dataObject"/> and writes the XML document to a file.
    /// </summary>
    /// <typeparam name="type">The type of the object to be serialized and saved in the file.</typeparam>
    /// <param name="dataObject">The object containing working data to be serialized and saved in the file.</param>
    /// <param name="path">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="mode">Specifies how the operating system should open a file.</param>
    public static void WriteXmlFile<type>(type dataObject, string path, FileMode mode)
      where type : IStylesheetNameProvider
    {
      XmlFile.WriteXmlFile<type>(dataObject, path, mode, dataObject.StylesheetName);
    }
    /// <summary>
    /// Reads an XML document from the file <paramref name="path"/> and deserializes its content to returned object.
    /// </summary>
    /// <typeparam name="type">The type of the object to be deserialized and saved in the file.</typeparam>
    /// <param name="path">A relative or absolute path for the file containing the serialized object.</param>
    /// <returns>An object containing working data retrieved from an XML file.</returns>
    /// <exception cref="System.ArgumentNullException">path</exception>
    public static type ReadXmlFile<type>(string path)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException(nameof(path));
      type _content = default(type);
      XmlSerializer _xmlSerializer = new XmlSerializer(typeof(type));
      FileStream _docStream = new FileStream(path, FileMode.Open, FileAccess.Read);
      using (XmlReader _writer = XmlReader.Create(_docStream))
        _content = (type)_xmlSerializer.Deserialize(_writer);
      return _content;
    }
    #endregion

  }
}
