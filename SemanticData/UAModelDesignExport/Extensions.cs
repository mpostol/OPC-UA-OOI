//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________


using System;
using System.Collections.Generic;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.InformationModelFactory.UAConstants;

namespace UAOOI.SemanticData.UAModelDesignExport
{

  /// <summary>
  /// Class Extensions - provides helper extension functions
  /// </summary>
  internal static class Extensions
  {

    /// <summary>
    /// Creates symbolics the name joining symbolic names on the path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>System.String.</returns>
    internal static string SymbolicName(this List<string> path)
    {
      path.Reverse();
      return String.Join("_", path.ToArray());
    }
    internal static void AddLocalizedText(string keyField, string valueField, ref XML.LocalizedText description, Action<TraceMessage> traceEvent)
    {
      if (description != null)
      {
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, "Parameter.Description does not supports array. The description is skipped."));
        return;
      }
      description = new XML.LocalizedText()
      {
        Key = keyField,
        Value = valueField,
      };
    }
    /// <summary>
    /// Gets the value rank.
    /// </summary>
    /// <param name="valueRank">The value rank.</param>
    /// <param name="specified">if set to <c>true</c> the parameter is specified.</param>
    /// <param name="traceEvent">An <see cref="Action"/> delegate is used to trace event as the <see cref="TraceMessage"/>.</param>
    /// <returns>Returns value of <see cref="XML.ValueRank" />.</returns>
    internal static XML.ValueRank GetValueRank(this int? valueRank, Action<bool> specified, Action<TraceMessage> traceEvent)
    {
      XML.ValueRank _vr = XML.ValueRank.Scalar;
      if (!valueRank.HasValue)
      {
        specified(false);
        return _vr;
      }
      specified(true);
      if (valueRank < -2)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongValueRank, String.Format("The value {0} is not supported", valueRank)));
      else if (valueRank == -3)
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.WrongValueRank, String.Format("The value {0} is not supported", valueRank)));
      //_vr = ModelDesign.ValueRank.ScalarOrOneDimension;
      else if (valueRank == -2)
        _vr = XML.ValueRank.ScalarOrArray;
      else if (valueRank == -1)
      {
        _vr = XML.ValueRank.Scalar;
        specified(false);
      }
      else if (valueRank == 0)
        _vr = XML.ValueRank.OneOrMoreDimensions;
      else if (valueRank == 1)
        _vr = XML.ValueRank.Array;
      else
        _vr = XML.ValueRank.OneOrMoreDimensions;
      return _vr;
    }
    internal static XML.AccessLevel GetAccessLevel(this uint? accessLevel, Action<bool> accessLevelSpecified, Action<TraceMessage> traceEvent)
    {
      XML.AccessLevel _ret = XML.AccessLevel.None;
      if (!accessLevel.HasValue)
      {
        accessLevelSpecified(false);
        return _ret;
      }
      if (accessLevel == AccessLevels.CurrentReadOrWrite)
        _ret = XML.AccessLevel.ReadWrite;
      else if (accessLevel == AccessLevels.CurrentRead)
        _ret = XML.AccessLevel.Read;
      else if (accessLevel == AccessLevels.CurrentWrite)
        _ret = XML.AccessLevel.Write;
      else
        traceEvent(TraceMessage.BuildErrorTraceMessage(BuildError.NotSupportedFeature, String.Format("The AccessLevel value {0:X} is not supported", accessLevel)));
      accessLevelSpecified((int)_ret != 1);
      return _ret;
    }
    internal static string Key(this XML.Reference value)
    {
      return value.ReferenceType.ToString() + " " + value.TargetId.ToString();
    }
    //Extensions for UAOOI
    internal static XML.Parameter ExportArgument(this Parameter argument, Action<TraceMessage> traceEvent)
    {
      bool _ValueRankSpecified = false;
      XML.ValueRank _ValueRank = argument.ValueRank.GetValueRank(x => _ValueRankSpecified = x, traceEvent);
      return new XML.Parameter()
      {
        DataType = argument.DataType,
        Description = argument.Descriptions == null || argument.Descriptions.Count == 0 ? null : new XML.LocalizedText() { Key = argument.Descriptions[0].Locale, Value = argument.Descriptions[0].Text },
        Identifier = 0,
        IdentifierSpecified = false,
        Name = argument.Name,
        ValueRank = _ValueRank,
      };
    }
    internal static XML.ModellingRule ConvertModellingRule(this ModelingRules? modellingRule, Action<bool> isSpecified)
    {
      XML.ModellingRule _ret = default(XML.ModellingRule);
      if (!modellingRule.HasValue)
      {
        isSpecified(false);
        return _ret;
      }
      switch (modellingRule)
      {
        case ModelingRules.Mandatory:
          _ret = XML.ModellingRule.Mandatory;
          break;
        case ModelingRules.Optional:
          _ret = XML.ModellingRule.Optional;
          break;
        case ModelingRules.MandatoryPlaceholder:
          _ret = XML.ModellingRule.MandatoryPlaceholder;
          break;
        case ModelingRules.OptionalPlaceholder:
          _ret = XML.ModellingRule.OptionalPlaceholder;
          break;
        case ModelingRules.ExposesItsArray:
          _ret = XML.ModellingRule.ExposesItsArray;
          break;
      }
      isSpecified(true);
      return _ret;
    }
    internal static XML.ReleaseStatus ConvertToReleaseStatus(this ReleaseStatus releaseStatus)
    {
      XML.ReleaseStatus _status = XML.ReleaseStatus.Released;
      switch (releaseStatus)
      {
        case ReleaseStatus.Released:
          _status = XML.ReleaseStatus.Released;
          break;
        case ReleaseStatus.Draft:
          _status = XML.ReleaseStatus.Draft;
          break;
        case ReleaseStatus.Deprecated:
          _status = XML.ReleaseStatus.Deprecated;
          break;
      }
      return _status;
    }
  }

}
