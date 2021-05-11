//__________________________________________________________________________________________________
//
//  Copyright (C) 2021, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UAOOI.Common.Infrastructure.Diagnostic;
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
      Program program = new Program();
      try
      {
        AssemblyName myAssembly = Assembly.GetExecutingAssembly().GetName();
        AssemblyName = $"Address Space Prototyping (asp.exe) Version {myAssembly.Version}";
        program.traceSource.TraceSource.TraceEvent(TraceEventType.Information, 1637887218, AssemblyName);
        program.traceSource.TraceSource.TraceEvent(TraceEventType.Information, 1637887219, Copyright);
        program.Run(args);
      }
      catch (Exception ex)
      {
        string errorMessage = $"Program stopped by the exception: {ex.Message}";
        Console.WriteLine(errorMessage);
        program.traceSource.TraceSource.TraceEvent(TraceEventType.Critical, 828896092, errorMessage);
        Environment.Exit(1);
      }
    }

    internal void Run(string[] args)
    {
      try
      {
        args.Parse<Options>(Do, HandleErrors);
      }
      catch (Exception ex)
      {
        Console.WriteLine(string.Format("Program stopped by the exception: {0}", ex.Message));
        throw;
      }
    }

    internal void Do(Options options, IAddressSpaceContext addressSpace)
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

    private readonly TraceSourceBase traceSource = new TraceSourceBase("AddressSpacePrototyping");
    private const string Copyright = "Copyright(c) 2021 Mariusz Postol";
    private static string AssemblyName = String.Empty;

    private void HandleErrors(IEnumerable<Error> errors)
    {
      foreach (Error _item in errors)
      {
        string _processing = _item.StopsProcessing ? "and it stops processing" : "but the processing continues";
        string errorMessage = $"The list of command line parameters has the error: {_item.ToString()} {_processing}.";
        traceSource.TraceSource.TraceEvent(TraceEventType.Error, 1230327407, errorMessage);
        Console.WriteLine(errorMessage);
      }
    }

    private void Do(Options options)
    {
      //TOD Enhance/Improve the Program logging and tracing infrastructure. #590
      PrintLogo(options.NoLogo);
      BuildErrorsHandling.Log.TraceEventAction += z => Console.WriteLine(z.ToString());
      BuildErrorsHandling.Log.TraceEventAction += y => traceSource.TraceSource.TraceEvent(y.TraceLevel, 566981851, y.ToString());
      IAddressSpaceContext addressSpace = AddressSpaceFactory.AddressSpace;  //Creates Address Space infrastructure exposed to the API clients using default messages handler.
      Do(options, addressSpace);
    }

    private void PrintLogo(bool nologo)
    {
      if (nologo)
        return;
      Console.WriteLine(AssemblyName);
      Console.WriteLine(Copyright);
      Console.WriteLine();
    }

    #endregion private

    #region DEBUG

    [Conditional("DEBUG")]
    internal void GetTraceSource(Action<TraceSource> geter)
    {
      geter(traceSource.TraceSource);
    }

    #endregion DEBUG
  }
}