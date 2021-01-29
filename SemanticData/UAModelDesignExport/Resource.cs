//___________________________________________________________________________________
//
//  Copyright (C) 2921, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace UAOOI.SemanticData.UAModelDesignExport.XML
{
  /// <summary>
  /// Resources management helper class
  /// </summary>
  public static class UAResources
  {
    /// <summary>
    /// Loads the OPC UA defined types.
    /// </summary>
    /// <returns>An instance of <see cref="ModelDesign"/> representing  UA defined types</returns>
    public static ModelDesign LoadUADefinedTypes()
    {
      return LoadResource<ModelDesign>(UADefinedTypesName());
    }

    private static readonly Func<string> UADefinedTypesName = () => $"{typeof(UAResources).Namespace}.XML.UA Defined Types.xml";

    /// <summary>
    /// Loads a schema from an embedded resource.
    /// </summary>
    private static type LoadResource<type>(string path)
    {
      try
      {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(path)))
        {
          XmlSerializer serializer = new XmlSerializer(typeof(type));
          return (type)serializer.Deserialize(reader);
        }
      }
      catch (Exception e)
      {
        throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, "Could not load resource '{0}' because the exception {1} reports the error {2}.", path, e.GetType().Name, e.Message), e);
      }
    }
  }
}