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
        program.traceSource.TraceData(TraceEventType.Information, 1637887218, AssemblyName);
        program.traceSource.TraceData(TraceEventType.Information, 1637887219, Copyright);
        program.Run(args);
      }
      catch (Exception ex)
      {
        string errorMessage = $"Program stopped by the exception: {ex.Message}";
        Console.WriteLine(errorMessage);
        program.traceSource.TraceData(TraceEventType.Critical, 828896092, errorMessage);
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
        addressSpace.InformationModelFactory = exporter.GetFactory();  //Sets the information model factory, which can be used to export a part of the OPC UA Address Space.
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
        {
          string message = $"The file {_fileToRead.FullName} doesn't exist.";
          traceSource.TraceData(TraceEventType.Critical, 1637887215, message);
          throw new FileNotFoundException(message, _path);
        }
        traceSource.TraceData(TraceEventType.Verbose, 1637887216, $"Importing UANodeSet document from file {_fileToRead.FullName}");
        addressSpace.ImportUANodeSet(_fileToRead);
      }
      traceSource.TraceData(TraceEventType.Verbose, 1637887217, $"Validating and exporting a model from namespace {uri}");
      addressSpace.ValidateAndExportModel(uri); //Validates and exports the selected model.
      if (_exportModel)
      {
        traceSource.TraceData(TraceEventType.Verbose, 1637887217, $"Writing model to XML file {options.ModelDesignFileName}");
        exporter.ExportToXMLFile(options.ModelDesignFileName, options.Stylesheet); //Serializes the already generated model and writes the XML document to a file.
      }
    }

    internal ITraceSource DebugITraceSource { set => traceSource = value; }

    #region private

    private ITraceSource traceSource = new TraceSourceBase("AddressSpacePrototyping");
    private const string Copyright = "Copyright(c) 2021 Mariusz Postol";
    private static string AssemblyName = String.Empty;

    private void HandleErrors(IEnumerable<Error> errors)
    {
      foreach (Error _item in errors)
      {
        string _processing = _item.StopsProcessing ? "and it stops processing" : "but the processing continues";
        string errorMessage = $"The list of command line parameters has the error: {_item.ToString()} {_processing}.";
        traceSource.TraceData(TraceEventType.Error, 1230327407, errorMessage);
        Console.WriteLine(errorMessage);
      }
    }

    private void Do(Options options)
    {
      PrintLogo(options.NoLogo);
      traceSource.TraceData(TraceEventType.Verbose, 6710129, "Creating Address Space populated using Standard Model. It will take a while ...");
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
    internal void GetTraceSource(Action<ITraceSource> geter)
    {
      geter(traceSource);
    }

    #endregion DEBUG
  }
}