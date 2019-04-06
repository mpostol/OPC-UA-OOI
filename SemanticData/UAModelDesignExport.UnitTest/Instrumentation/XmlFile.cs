//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace UAOOI.SemanticData.UAModelDesignExport.Instrumentation
{
  /// <summary>
  /// Provides static methods for serialization objects into XML documents and writing the XML document to a file.
  /// </summary>
  public static class XmlFile
  {

    #region public
    /// <summary>
    /// A structure containing dat to be serialized
    /// </summary>
    public struct DataToSerialize<Type4Serialization>
    {
      /// <summary>
      /// The <see cref="XmlSerializerNamespaces"/> referenced by the object.
      /// </summary>
      public XmlSerializerNamespaces XmlNamespaces;
      /// <summary>
      /// The object containing working data to be serialized and saved in the file.
      /// </summary>
      public Type4Serialization Data;
      /// <summary>
      ///Name of the stylesheet document.
      /// </summary>
      public string StylesheetName;
    }
    /// <summary>
    /// Serializes the specified <paramref name="dataObject" /> and writes the XML document to a file.
    /// </summary>
    /// <typeparam name="type"> type of the root object to be serialized and saved in the file.</typeparam>
    /// <param name="dataObject">The structure containing working data to be serialized and saved in the file.</param>
    /// <param name="path">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="mode">Specifies how the operating system should open a file <see cref="FileMode" />.</param>
    public static void WriteXmlFile<type>(DataToSerialize<type> dataObject, string path, FileMode mode)
    {
      WriteXmlFile<type>(dataObject.Data, path, mode, dataObject.StylesheetName, dataObject.XmlNamespaces);
    }
    /// <summary>
    /// Serializes the specified <paramref name="dataObject" /> and writes the XML document to a file.
    /// </summary>
    /// <typeparam name="type">The type of the root object to be serialized and saved in the file.</typeparam>
    /// <param name="dataObject">The object containing working data to be serialized and saved in the file.</param>
    /// <param name="path">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="mode">Specifies how the operating system should open a file <see cref="FileMode" />.</param>
    /// <param name="stylesheetName">Name of the stylesheet document.</param>
    /// <exception cref="System.ArgumentNullException"><paramref name="path"/> or <paramref name="dataObject"/> stylesheetName</exception>
    public static void WriteXmlFile<type>(type dataObject, string path, FileMode mode, string stylesheetName)
    {
      WriteXmlFile<type>(dataObject, path, mode, stylesheetName, null);
    }
    /// <summary>
    /// Serializes the specified <paramref name="dataObject" /> and writes the XML document to a file.
    /// </summary>
    /// <typeparam name="type">The type of the root object to be serialized and saved in the file.</typeparam>
    /// <param name="dataObject">The object containing working data to be serialized and saved in the file.</param>
    /// <param name="path">A relative or absolute path for the file containing the serialized object.</param>
    /// <param name="mode">Specifies how the operating system should open a file <see cref="FileMode" />.</param>
    /// <param name="stylesheetName">Name of the stylesheet document.</param>
    /// <param name="xmlNamespaces">The <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> referenced by the object.</param>
    /// <exception cref="System.ArgumentNullException"><paramref name="path"/> or <paramref name="dataObject"/> stylesheetName</exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    public static void WriteXmlFile<type>(type dataObject, string path, FileMode mode, string stylesheetName, XmlSerializerNamespaces xmlNamespaces)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException("path");
      if (dataObject == null)
        throw new ArgumentNullException("content");
      XmlSerializer _srlzr = new XmlSerializer(typeof(type));
      XmlWriterSettings _setting = new XmlWriterSettings()
      {
        Indent = true,
        IndentChars = "  ",
        NewLineChars = "\r\n"
      };
      FileStream _docStrm = new FileStream(path, mode, FileAccess.Write);
      using (XmlWriter _writer = XmlWriter.Create(_docStrm, _setting))
      {
        if (!string.IsNullOrEmpty(stylesheetName))
          _writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" " + String.Format("href=\"{0}\"", stylesheetName));
        _srlzr.Serialize(_writer, dataObject, xmlNamespaces);
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
      WriteXmlFile<type>(dataObject, path, mode, dataObject.StylesheetName);
    }
    /// <summary>
    /// Reads an XML document from the file <paramref name="path"/> and deserializes its content to returned object.
    /// </summary>
    /// <typeparam name="type">The type of the object to be deserialized and saved in the file.</typeparam>
    /// <param name="path">A relative or absolute path for the file containing the serialized object.</param>
    /// <returns>An object containing working data retrieved from an XML file.</returns>
    /// <exception cref="System.ArgumentNullException">path</exception>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
    public static type ReadXmlFile<type>(string path)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException("path");
      type _content = default(type);
      XmlSerializer _srlzr = new XmlSerializer(typeof(type));
      using (FileStream _docStrm = new FileStream(path, FileMode.Open, FileAccess.Read))
      using (XmlReader _writer = XmlReader.Create(_docStrm))
        _content = (type)_srlzr.Deserialize(_writer);
      return _content;
    }
    #endregion

  }
}
