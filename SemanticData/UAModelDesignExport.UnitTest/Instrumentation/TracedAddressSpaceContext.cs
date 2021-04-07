//___________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UAModelDesignExport.XML;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.UAModelDesignExport.Instrumentation
{
  internal class TracedAddressSpaceContext : IDisposable
  {
    public ModelDesign CreateInstance(FileInfo filePath, string URI)
    {
      if (!filePath.Exists)
        throw new FileNotFoundException("The imported file does not exist", filePath.FullName);
      IAddressSpaceContext _as = AddressSpaceFactory.GetAddressSpace(TraceDiagnostic);
      ModelFactory _factory = new ModelFactory(TraceDiagnostic);
      _as.InformationModelFactory = _factory;
      _as.ImportUANodeSet(filePath);
      _as.ValidateAndExportModel(new Uri(URI));
      return _factory.Export();
    }

    internal readonly List<TraceMessage> TraceList = new List<TraceMessage>();
    internal int _diagnosticCounter = 0;

    public void Dispose()
    {
    }

    internal void Clear()
    {
      _diagnosticCounter = 0;
      TraceList.Clear();
    }

    private void TraceDiagnostic(TraceMessage msg)
    {
      Debug.WriteLine(msg.ToString());
      if (msg.BuildError.Focus == Focus.Diagnostic)
        _diagnosticCounter++;
      else
        TraceList.Add(msg);
    }
  }
}