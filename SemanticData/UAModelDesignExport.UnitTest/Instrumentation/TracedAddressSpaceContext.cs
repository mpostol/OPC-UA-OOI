//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UAModelDesignExport.XML;
using UAOOI.SemanticData.UANodeSetValidation;
using UAOOI.SemanticData.UANodeSetValidation.Diagnostic;

namespace UAOOI.SemanticData.UAModelDesignExport.Instrumentation
{
  internal class TracedAddressSpaceContext : IBuildErrorsHandling, IDisposable
  {
    public ModelDesign CreateInstance(FileInfo filePath, string URI)
    {
      if (!filePath.Exists)
        throw new FileNotFoundException("The imported file does not exist", filePath.FullName);
      IAddressSpaceContext _as = new AddressSpaceContext(this);
      ModelFactory _factory = new ModelFactory(WriteTraceMessage);
      _as.InformationModelFactory = _factory;
      _as.ImportUANodeSet(filePath);
      _as.ValidateAndExportModel(new Uri(URI));
      return _factory.Export();
    }

    internal readonly List<TraceMessage> TraceList = new List<TraceMessage>();

    public void Dispose()
    {
    }

    internal void Clear()
    {
      Errors = 0;
      TraceList.Clear();
    }

    #region IBuildErrorsHandling

    public int Errors { get; private set; }

    public void WriteTraceMessage(TraceMessage traceMessage)
    {
      Debug.WriteLine(traceMessage.ToString());
      if (traceMessage.BuildError.Focus == Focus.Diagnostic)
        return;
      Errors++;
      TraceList.Add(traceMessage);
    }

    public void TraceData(TraceEventType eventType, int id, object data)
    {
      throw new NotImplementedException("It is intentionally not implemented");
      if ((eventType == TraceEventType.Verbose) || (eventType == TraceEventType.Information))
        Errors++;
      else
      {
        string message = $"Unexpected error: eventType = {eventType} id = {id} data = {data}";
        Debug.WriteLine(message);
        //throw new ApplicationException(message);
      }
    }

    #endregion IBuildErrorsHandling
  }
}