//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommandLine;
using System.Collections.Generic;

namespace UAOOI.SemanticData.AddressSpaceTestTool.CommandLineSyntax
{
  internal class Options
  {
    [Value(0, Required = true, HelpText = "At least one file containing Address Space definition must be specified")]
    public IEnumerable<string> Filenames { get; set; }
    [Option('e', "export", HelpText = "Ecport to UA Model Designer XML file")]
    public string ModelDesignFileName { get; set; }
    [Option('c', "no-create", HelpText = "DontCreate")]
    public bool DontCreate { get; set; }
    [Option('a', HelpText = "change only the access time")]
    public bool OnlyAccessTime { get; set; }
    [Option('m', HelpText = "OnlyModificationTime")]
    public bool OnlyModificationTime { get; set; }

  }
}
