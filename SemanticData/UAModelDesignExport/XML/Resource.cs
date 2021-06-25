//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using UAOOI.Common.Infrastructure.Serializers;

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
      try
      {
        Assembly assembly = Assembly.GetExecutingAssembly();
        //TODO XmlFile.ReadXmlFile<type>(string) doesn't close the stream #619
        return XmlFile.ReadXmlFile<ModelDesign>(assembly.GetManifestResourceStream(UADefinedTypesName));
      }
      catch (Exception e)
      {
        throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, "Could not load resource '{0}' because the exception {1} reports the error {2}.", UADefinedTypesName, e.GetType().Name, e.Message), e);
      }
    }

    private static string UADefinedTypesName => $"{typeof(UAResources).Namespace}.UA Defined Types.xml";
  }
}