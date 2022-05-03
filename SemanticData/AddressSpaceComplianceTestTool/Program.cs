//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
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
    #region public API

    public static void Main(string[] args)
    {
      Program program = new Program();
      try
      {
        AssemblyName myAssembly = Assembly.GetExecutingAssembly().GetName();
        string assemblyHeader = $"Address Space Prototyping (asp.exe) Version {myAssembly.Version}";
        program.TraceSource.TraceData(TraceEventType.Information, 1637887218, assemblyHeader);
        program.TraceSource.TraceData(TraceEventType.Information, 1637887219, Copyright);
        program.Execute(args);
      }
      catch (Exception ex)
      {
        string errorMessage = $"Program stopped by the exception: {ex.Message}";
        Console.WriteLine(errorMessage);
        program.TraceSource.TraceData(TraceEventType.Critical, 828896092, errorMessage);
        Environment.Exit(1);
      }
    }

    internal async Task Run(string[] args)
    {
      try
      {
        //TODO Integrate with the UA-ModelCompiler #648 https://github.com/commandlineparser/commandline/wiki/Verbs
        await Task.Run(() => args.Parse<Options>(Do, HandleErrors));
      }
      catch (Exception ex)
      {
        Console.WriteLine(string.Format("Program stopped by the exception: {0}", ex.Message));
        throw;
      }
    }

    internal void Do(Options options, IAddressSpaceContext addressSpace)
    {
      IModelDesignExport exporter = ModelDesignExportAPI.GetModelDesignExport(); //creates new instance of the ModelDesignExport class that captures functionality supporting export of the OPC UA Information Model represented
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
          TraceSource.TraceData(TraceEventType.Critical, 1637887215, message);
          throw new FileNotFoundException(message, _path);
        }
        TraceSource.TraceData(TraceEventType.Verbose, 1637887216, $"Importing UANodeSet document from file {_fileToRead.FullName}");
        addressSpace.ImportUANodeSet(_fileToRead);
      }
      TraceSource.TraceData(TraceEventType.Verbose, 1637887217, $"Validating and exporting a model from namespace {uri}");
      addressSpace.ValidateAndExportModel(uri); //Validates and exports the selected model.
      if (_exportModel)
      {
        TraceSource.TraceData(TraceEventType.Verbose, 1637887217, $"Writing model to XML file {options.ModelDesignFileName}");
        exporter.ExportToXMLFile(options.ModelDesignFileName, options.Stylesheet); //Serializes the already generated model and writes the XML document to a file.
      }
    }

    internal ITraceSource DebugITraceSource { set => TraceSource = value; }

    #endregion public API

    #region private

    private ITraceSource TraceSource = new TraceSourceBase("AddressSpacePrototyping");
    private const string Copyright = "Copyright(c) 2022 Mariusz Postol";
    private bool Running = true;

    private void Execute(string[] args)
    {
      Task heartbeatTask = Heartbeat();
      Run(args).Wait();
      Running = false;
      heartbeatTask.Wait();
    }

    private async Task Heartbeat()
    {
      await Task.Run(async () =>
      {
        int counter = 0;
        while (Running)
        {
          await Task.Delay(1000);
          Console.Write("\r");
          if (counter % 2 == 0)
            Console.Write(@"\");
          else
            Console.Write("/");
          counter++;
        }
        TraceSource.TraceData(TraceEventType.Verbose, 918215642, $"Execution time = {counter}s");
        Console.WriteLine();
        Console.WriteLine($"Execution time = {counter}s");
      });
    }

    private void HandleErrors(IEnumerable<Error> errors)
    {
      foreach (Error _item in errors)
      {
        string _processing = _item.StopsProcessing ? "and it stops processing" : "but the processing continues";
        string errorMessage = $"The list of command line parameters has the error: {_item.ToString()} {_processing}.";
        TraceSource.TraceData(TraceEventType.Error, 1230327407, errorMessage);
        Console.WriteLine(errorMessage);
      }
    }

    private void Do(Options options)
    {
      PrintLogo(options.NoLogo);
      TraceSource.TraceData(TraceEventType.Verbose, 6710129, "Creating Address Space populated using Standard Model. It will take a while ...");
      //TODO Integrate with the UA-ModelCompiler #648
      if (true)
      {
        IAddressSpaceContext addressSpace = AddressSpaceFactory.AddressSpace;  //Creates Address Space infrastructure exposed to the API clients using default messages handler.
        Do(options, addressSpace);
      }
      else
        ;
    }

    private void PrintLogo(bool nologo)
    {
      if (nologo)
        return;
      Console.WriteLine(AssemblyHeader);
      Console.WriteLine(Copyright);
      Console.WriteLine();
    }

    #endregion private

    #region DEBUG

    [Conditional("DEBUG")]
    internal void GetTraceSource(Action<ITraceSource> geter)
    {
      geter(TraceSource);
    }

    #endregion DEBUG
  }
}