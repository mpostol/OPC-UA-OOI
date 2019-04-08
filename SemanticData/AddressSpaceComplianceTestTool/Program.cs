//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAOOI.SemanticData.AddressSpaceTestTool.CommandLineSyntax;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UAModelDesignExport;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.AddressSpaceTestTool
{
  internal class Program
  {
    internal static void Main(string[] args)
    {
      try
      {
        args.Parse<Options>(Do, HandleErrors);
      }
      catch (Exception ex)
      {
        Console.WriteLine(string.Format("Program stopped by the exception: {0}", ex.Message));
      }
      Console.Write("Press Enter to close this window.......");
      Console.Read();
    }
    private static void HandleErrors(IEnumerable<Error> errors)
    {
      foreach (Error _item in errors)
      {
        string _processing = _item.StopsProcessing ? "and it stops processing" : "but the processing continues";
        Console.WriteLine($"The following tag has wrong value: {_item.Tag} {_processing}.");
      }
    }
    private static void Do(Options obj)
    {
      Action<TraceMessage> _tracingMetchod = z => Console.WriteLine(z.ToString());
      FileInfo _fileToRead = GetFileToRead(obj.Filenames);
      IAddressSpaceContext _as = new AddressSpaceContext(_tracingMetchod);
      ModelDesignExport _exporter = new ModelDesignExport();
      bool _exportModel = false;
      if (!string.IsNullOrEmpty(obj.ModelDesignFileName))
      {
        _as.InformationModelFactory = _exporter.GetFactory(obj.ModelDesignFileName, _tracingMetchod);
        _exportModel = true;
      }
      _as.ImportUANodeSet(_fileToRead);
      _as.ValidateAndExportModel();
      if (_exportModel)
        _exporter.ExportToXMLFile();
    }
    internal static FileInfo GetFileToRead(IEnumerable<string> files)
    {
      if (files == null || files.Count<string>() != 1)
        throw new ArgumentOutOfRangeException("args", "List of command line arguments is incorrect - enter name of an xml file to be tested.");
      FileInfo _FileInfo = new FileInfo(files.First<string>());
      if (!_FileInfo.Exists)
        throw new FileNotFoundException(string.Format("FileNotFoundException - the file {0} doesn't exist.", _FileInfo.FullName));
      return _FileInfo;
    }
    //Command lines
    //"XMLModels\DataTypeTest.NodeSet2.xml" -e "XMLModels\DataTypeTest.ModelDesign.xml"
  }

}

