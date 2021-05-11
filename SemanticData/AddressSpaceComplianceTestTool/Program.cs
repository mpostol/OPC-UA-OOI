//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UAOOI.SemanticData.AddressSpacePrototyping.CommandLineSyntax;
using UAOOI.SemanticData.UAModelDesignExport;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.AddressSpacePrototyping
{
  /// <summary>
  /// Class Program - main entry point to the OPC UA Address Space Prototyping tool (asp.exe)
  /// </summary>
  public class Program
  {
    public static void Main(string[] args)
    {
      try
      {
        Run(args);
      }
      catch (Exception ex)
      {
        //TODO Enhance/Improve the Program logging and tracing infrastructure. #590
        Console.WriteLine(string.Format("Program stopped by the exception: {0}", ex.Message));
      }
    }

    internal static void Run(string[] args)
    {
      args.Parse<Options>(Do, HandleErrors);
    }

    private static void HandleErrors(IEnumerable<Error> errors)
    {
      foreach (Error _item in errors)
      {
        //TOD Enhance/Improve the Program logging and tracing infrastructure. #590
        string _processing = _item.StopsProcessing ? "and it stops processing" : "but the processing continues";
        //TODO Enhance/Improve the Program logging and tracing infrastructure. #590
        //Console.WriteLine($"The following tag has wrong value: {_item.Tag} {_processing}.");
      }
    }

    internal static void Do(Options options, IAddressSpaceContext addressSpace)
    {
      ModelDesignExport exporter = new ModelDesignExport(); //creates new instance of the ModelDesignExport class that captures functionality supporting export of the OPC UA Information Model represented
                                                            //by an XML file compliant with UAModelDesign schema.
      bool _exportModel = false;
      if (!string.IsNullOrEmpty(options.ModelDesignFileName))
      {
        addressSpace.InformationModelFactory = exporter.GetFactory(BuildErrorsHandling.Log.TraceEvent);  //Sets the information model factory, which can be used to export a part of the OPC UA Address Space.
        _exportModel = true;
      }
      if (options.Filenames == null)
        throw new ArgumentOutOfRangeException($"{nameof(options.Filenames)}", "List of input files to convert is incorrect. At least one file UANodeSet must be entered.");
      if (string.IsNullOrEmpty(options.IMNamespace))
        throw new ArgumentOutOfRangeException("namespace", "A namespace must be provided to validate associated model");
      Uri uri = new Uri(options.IMNamespace);
      foreach (string _path in options.Filenames)
      {
        FileInfo _fileToRead = new FileInfo(_path);
        if (!_fileToRead.Exists)
          throw new FileNotFoundException(string.Format($"FileNotFoundException - the file {_path} doesn't exist.", _fileToRead.FullName));
        addressSpace.ImportUANodeSet(_fileToRead);
      }
      addressSpace.ValidateAndExportModel(uri); //Validates and exports the selected model.
      if (_exportModel)
        exporter.ExportToXMLFile(options.ModelDesignFileName, options.Stylesheet); //Serializes the already generated model and writes the XML document to a file.
    }

    #region private

    private static void Do(Options options)
    {
      //TOD Enhance/Improve the Program logging and tracing infrastructure. #590
      PrintLogo(options);
      BuildErrorsHandling.Log.TraceEventAction += z => Console.WriteLine(z.ToString());
      IAddressSpaceContext addressSpace = AddressSpaceFactory.AddressSpace;  //Creates Address Space infrastructure exposed to the API clients using default messages handler.
      Do(options, addressSpace);
    }

    private static void PrintLogo(Options options)
    {
      if (options.NoLogo)
        return;
      AssemblyName _myAssembly = Assembly.GetExecutingAssembly().GetName();
      Console.WriteLine($"Address Space Prototyping (asp.exe) {_myAssembly.Version}");
      Console.WriteLine("Copyright(c) 2021 Mariusz Postol");
      Console.WriteLine();
    }

    #endregion private
  }
}