//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.IO;
using UAOOI.SemanticData.UANodeSetValidation;

namespace UAOOI.SemanticData.AddressSpaceTestTool
{
  class Program
  {
    internal static void Main(string[] args)
    {

      try
      {
        FileInfo _fileToRead = GetFileToRead(args);
        ValidateFile(_fileToRead);
      }
      catch (Exception ex)
      {
        Console.WriteLine(String.Format("Program stopped by the exception: {0}", ex.Message));
      }
      Console.Write("Press Enter to close this window.......");
      Console.Read();
    }
    internal static void ValidateFile(FileInfo _fileToRead)
    {
      IAddressSpaceContext _as = new AddressSpaceContext(z => Console.WriteLine(z.ToString()));
      _as.ImportUANodeSet(_fileToRead);
      _as.ValidateAndExportModel();
    }
    internal static FileInfo GetFileToRead(string[] args)
    {
      if (args == null || args.Length != 1)
        throw new ArgumentOutOfRangeException("args", "List of command line arguments is incorrect - enter name of an xml file to be tested.");
      FileInfo _FileInfo = new FileInfo(args[0]);
      if (!_FileInfo.Exists)
        throw new FileNotFoundException(String.Format("FileNotFoundException - the file {0} doesn't exist.", args[0]));
      return _FileInfo;
    }
  }
}
