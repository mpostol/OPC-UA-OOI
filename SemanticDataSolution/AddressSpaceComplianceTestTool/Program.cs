
using System;
using System.IO;

namespace UAOOI.SemanticData.AddressSpaceTestTool
{
  class Program
  {
    internal static void Main(string[] args)
    {
      FileInfo _fileToRead = GetFileToRead(args);
      ValidateFile(_fileToRead);
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
