
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using UAOOI.SemanticData.UANodeSetValidation.XML;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.UAInformationModel;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Class Extensions - provides helper functions for this namespace
  /// </summary>
  internal static class Extensions
  {
    //string
    internal static string SymbolicName(this List<string> path)
    {
      return String.Join("_", path.ToArray());
    }
    /// <summary>
    /// Exports the string and filter out the default value. 
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>Returns <paramref name="value"/> if not equal to <paramref name="defaultValue"/>, otherwise it returns <see cref="String.Empty"/>.</returns>
    internal static string ExportString(this string value, string defaultValue)
    {
      if (String.IsNullOrEmpty(value))
        return null;
      return String.Compare(value, defaultValue) == 0 ? null : value;
    }
    internal static type Export<type>(this type value, type defaultValue, Action<bool> isSpecified)
      where type : IEquatable<type>
    {
      isSpecified(!value.Equals(defaultValue));
      return value;
    }
    internal static UInt32 Validate(this UInt32 value, UInt32 maxValue, Action<UInt32> reportError)
    {
      if (value.CompareTo(maxValue) >= 0)
        reportError(value);
      return value & maxValue - 1;
    }
    internal static string ValidateIdentifier(this string name, Action<TraceMessage> reportError)
    {
      if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(name))
        reportError(TraceMessage.BuildErrorTraceMessage(BuildError.WrongSymbolicName, String.Format("SymbolicName: '{0}'.", name)));
      return name;
    }
    internal static string NodeIdentifier(this UANode node)
    {
      if (String.IsNullOrEmpty(node.BrowseName))
        return node.SymbolicName;
      return node.BrowseName;
    }
    internal static string ConvertToString(this XML.LocalizedText[] localizedText)
    {
      if (localizedText == null || localizedText.Length == 0)
        return "Empty LocalizedText";
      return String.Format("{0}:{1}", localizedText[0].Locale, localizedText[0].Value);
    }
    /// <summary>
    /// Loads a schema from an embedded resource.
    /// </summary>
    internal static type LoadResource<type>(this string path)
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
        throw new FileNotFoundException(String.Format(CultureInfo.InvariantCulture, "Could not load resource '{0}' because the exception {1} reports the error {2}.", path, e.GetType().Name, e.Message), e);
      }
    }
    internal static void GetParameters(this DataTypeDefinition dataTypeDefinition, IExportDataTypeDefinitionFactory _definition, IUAModelContext modelContext, Action<TraceMessage> traceEvent)
    {
      if (dataTypeDefinition == null || dataTypeDefinition.Field == null || dataTypeDefinition.Field.Length == 0)
        return;
      foreach (DataTypeField _item in dataTypeDefinition.Field)
      {
        bool _ValueRankSpecified;
        IExportDataTypeFieldFactory _nP = _definition.Field();
        _nP.DataType = modelContext.ExportNodeId(_item.DataType, DataTypes.BaseDataType, traceEvent);
        _nP.Description = _item.Description;
        _nP.Identifier = _item.Value;
        _nP.IdentifierSpecified = true;
        _nP.Name = _item.Name;
        _nP.ValueRank = _item.ValueRank.GetValueRank(x => _ValueRankSpecified = x, traceEvent);
        if (_item.Definition != null)
          throw new NotImplementedException("Definition");
        _item.Definition.GetParameters(_nP.Definition(), modelContext, traceEvent);
        _nP.SymbolicName = _item.SymbolicName;
        _nP.Value = _item.Value;
      }
    }
    internal static XML.LocalizedText[] Truncate(this XML.LocalizedText[] localizedText, int maxLength, Action<TraceMessage> reportError)
    {
      if (localizedText == null || localizedText.Length == 0)
        return null;
      List<XML.LocalizedText> _ret = new List<XML.LocalizedText>();
      foreach (XML.LocalizedText _item in localizedText)
      {
        if (_item.Value.Length > maxLength)
        {
          reportError(TraceMessage.BuildErrorTraceMessage(BuildError.WrongDisplayNameLength, String.Format
            ("The localized text starting with '{0}:{1}' of length {2} is too long.", _item.Locale, _item.Value.Substring(0, 20), _item.Value.Length)));
          XML.LocalizedText _localizedText = new XML.LocalizedText()
          {
            Locale = _item.Locale,
            Value = _item.Value.Substring(0, maxLength)
          };
        }
      }
      return localizedText;
    }

  }
}
