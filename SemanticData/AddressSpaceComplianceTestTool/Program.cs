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
using System.Reflection;
using UAOOI.SemanticData.AddressSpacePrototyping.CommandLineSyntax;
using UAOOI.SemanticData.BuildingErrorsHandling;
using UAOOI.SemanticData.UAModelDesignExport;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.AddressSpacePrototyping
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
        //TODO trace it to log Console.WriteLine($"The following tag has wrong value: {_item.Tag} {_processing}.");
      }
    }
    private static void Do(Options options)
    {
      PrintLogo(options);
      Action<TraceMessage> _tracingMethod = z => Console.WriteLine(z.ToString());
      IAddressSpaceContext _as = new AddressSpaceContext(_tracingMethod);
      ModelDesignExport _exporter = new ModelDesignExport();
      bool _exportModel = false;
      if (!string.IsNullOrEmpty(options.ModelDesignFileName))
      {
        _as.InformationModelFactory = _exporter.GetFactory(options.ModelDesignFileName, _tracingMethod);
        _exportModel = true;
      }
      if (options.Filenames == null)
        throw new ArgumentOutOfRangeException($"{nameof(options.Filenames)}", "List of input files to convert i incorrect. At least one file UANodeSet must be entered.");
      foreach (string _path in options.Filenames)
      {
        FileInfo _fileToRead = new FileInfo(_path);
        if (!_fileToRead.Exists)
          throw new FileNotFoundException(string.Format($"FileNotFoundException - the file {_path} doesn't exist.", _fileToRead.FullName));
        _as.ImportUANodeSet(_fileToRead);
      }
      _as.ValidateAndExportModel(options.IMNamespace);
      if (_exportModel)
        _exporter.ExportToXMLFile(options.Stylesheet);
    }
    private static void PrintLogo(Options options)
    {
      if (options.NoLogo)
        return;
      AssemblyName _myAssembly = Assembly.GetExecutingAssembly().GetName();
      Console.WriteLine($"Address Space Prototyping (asp.exe) {_myAssembly.Version}");
      Console.WriteLine("Copyright(c) 2019 Mariusz Postol");
      Console.WriteLine();
    }
  }

}

