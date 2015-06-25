
using System;
using System.IO;

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
        Console.WriteLine(String.Format("Program stoped by the exception: {0}", ex.Message));
      }
      Console.Write("Press ENter to close this window.......");
      Console.Read();
    }
    internal static void ValidateFile(FileInfo _fileToRead)
    {
      throw new NotImplementedException();
    }
    internal static FileInfo GetFileToRead(string[] args)
    {
      throw new NotImplementedException();
    }
  }
}
