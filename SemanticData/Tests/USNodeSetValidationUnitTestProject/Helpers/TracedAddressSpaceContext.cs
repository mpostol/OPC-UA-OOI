//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UAOOI.SemanticData.AddressSpace.Abstractions;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.InformationModelFactory;
using UAOOI.SemanticData.UANodeSetValidation.DataSerialization;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;
using UAOOI.SemanticData.UANodeSetValidation.XML;

namespace UAOOI.SemanticData.UANodeSetValidation.Helpers
{
  /// <summary>
  /// Traced Address Space
  /// Implements the <seealso cref="IBuildErrorsHandling"/> interface
  /// </summary>
  ///
  internal class TracedAddressSpaceContext : IBuildErrorsHandling
  {
    /// <summary>
    /// Setup new instance
    /// - creates <see cref="IAddressSpaceContext"/>
    /// - reads standard model
    /// - reads custom model if not null
    /// </summary>
    /// <param name="testDataFileInfo">Custom model for testing purpose</param>
    internal TracedAddressSpaceContext(FileInfo testDataFileInfo)
    {
      AddressSpace = AddressSpaceFactory.AddressSpace(this);
      IUANodeSet iUANodeSet = UANodeSet.ReadUADefinedTypes();
      AddressSpace.ImportUANodeSet(iUANodeSet);
      if (testDataFileInfo != null)
      {
        Assert.IsTrue(testDataFileInfo.Exists);
        iUANodeSet = UANodeSet.ReadModelFile(testDataFileInfo);
        AddressSpace.ImportUANodeSet(iUANodeSet);
      }
    }

    internal IAddressSpaceContext AddressSpace = null;
    internal readonly List<TraceMessage> TraceList = new List<TraceMessage>();

    internal void UTAddressSpaceCheckConsistency(Action<IUANodeContext> action)
    {
      ((AddressSpaceContext)AddressSpace).UTAddressSpaceCheckConsistency(action);
    }

    internal void UTTryGetUANodeContext(DataSerialization.NodeId nodeId, Action<IUANodeContext> returnValue)
    {
      ((AddressSpaceContext)AddressSpace).UTTryGetUANodeContext(nodeId, returnValue);
    }

    internal void ValidateAndExportModel(Uri targetNamespace, IModelFactory factory = null)
    {
      AddressSpace.ValidateAndExportModel(targetNamespace, factory);
    }

    internal void UTReferencesCheckConsistency(Action<IUANodeContext, IUANodeContext, IUANodeContext, IUANodeContext> action)
    {
      ((AddressSpaceContext)AddressSpace).UTReferencesCheckConsistency(action);
    }

    internal void UTGetReferences(NodeId rootFolder, Action<UAReferenceContext> value)
    {
      ((AddressSpaceContext)AddressSpace).UTGetReferences(rootFolder, value);
    }

    internal void UTValidateAndExportModel(int nameSpaceIndex, Action<IEnumerable<IUANodeContext>> value)
    {
      ((AddressSpaceContext)AddressSpace).UTValidateAndExportModel(nameSpaceIndex, value);
    }

    internal IUANodeContext GetOrCreateNodeContext(NodeId nodeId, Func<NodeId, IUANodeContext> createUAModelContext)
    {
      return ((AddressSpaceContext)AddressSpace).GetOrCreateNodeContext(nodeId, createUAModelContext);
    }

    internal void GetBaseTypes(IUANodeContext hasPropertyNode, List<IUANodeContext> inheritanceChain)
    {
      ((AddressSpaceContext)AddressSpace).GetBaseTypes(hasPropertyNode, inheritanceChain);
    }

    internal void TestConsistency(int errorsCounter)
    {
      Assert.AreEqual<int>(errorsCounter, TraceList.Count);
    }

    internal void Clear()
    {
      Errors = 0;
      TraceList.Clear();
    }

    #region IBuildErrorsHandling

    public int Errors { get; set; } = 0;

    public void TraceData(TraceEventType eventType, int id, object data)
    {
      throw new NotImplementedException($"{nameof(TraceData)} must not be used");
    }

    public void WriteTraceMessage(TraceMessage traceMessage)
    {
      Console.WriteLine(traceMessage.ToString());
      if (traceMessage.BuildError.Focus == Focus.Diagnostic)
        return;
      Errors++;
      TraceList.Add(traceMessage);
    }

    #endregion IBuildErrorsHandling
  }
}