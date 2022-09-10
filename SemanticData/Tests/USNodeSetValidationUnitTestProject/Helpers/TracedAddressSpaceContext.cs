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
using System.Linq;
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
  internal class TracedAddressSpaceContext
  {
    /// <summary>
    /// Setup new instance
    /// - creates <see cref="IAddressSpaceContext"/>
    /// - reads standard model
    /// - reads custom model
    /// </summary>
    /// <param name="iUANodeSet">Custom model for testing purpose</param>
    internal TracedAddressSpaceContext(IUANodeSet iUANodeSet) : this()
    {
      Assert.IsNotNull(iUANodeSet);
      AddressSpace.ImportUANodeSet(iUANodeSet);
    }

    /// <summary>
    /// Setup new instance
    /// - creates <see cref="IAddressSpaceContext"/>
    /// - reads standard model
    /// - reads custom model
    /// </summary>
    /// <param name="path">Custom model for testing purpose</param>
    internal TracedAddressSpaceContext(string path) : this()
    {
      FileInfo testDataFileInfo = new FileInfo(path);
      Assert.IsTrue(testDataFileInfo.Exists);
      IUANodeSet iUANodeSet = UANodeSet.ReadModelFile(testDataFileInfo);
      Assert.IsNotNull(iUANodeSet);
      AddressSpace.ImportUANodeSet(iUANodeSet);
    }

    /// <summary>
    /// Setup new instance
    /// - creates <see cref="IAddressSpaceContext"/>
    /// - reads standard model
    /// </summary>
    internal TracedAddressSpaceContext()
    {
      AddressSpace = AddressSpaceFactory.AddressSpace(Log);
      AddressSpace.ImportUANodeSet(UANodeSet.ReadUADefinedTypes());
    }

    internal TraceMessage this[int i] => Log.TraceList[i];
    internal IAddressSpaceContext AddressSpace { get; private set; }

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

    internal int Where(Func<TraceMessage, bool> predicate)
    {
      return Log.TraceList.Where<TraceMessage>(predicate).Count<TraceMessage>();
    }

    internal void TestConsistency(int errorsCounter)
    {
      Assert.AreEqual<int>(errorsCounter, Log.TraceList.Count);
    }

    internal void Clear()
    {
      Log.Clear();
    }

    #region private instrumentation

    private class BuildErrorsHandling : IBuildErrorsHandling
    {
      internal void Clear()
      {
        Errors = 0;
        TraceList.Clear();
      }

      internal List<TraceMessage> TraceList = new List<TraceMessage>();

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

    private BuildErrorsHandling Log = new BuildErrorsHandling();

    #endregion private instrumentation
  }
}