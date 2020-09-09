//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommandLine;
using System.Collections.Generic;

namespace UAOOI.SemanticData.AddressSpacePrototyping.CommandLineSyntax
{
  /// <summary>
  /// Class Options - defines command line switches used to control behavior of the application.
  /// </summary>
  internal class Options
  {
    [Value(0, Required = true, HelpText = "Specifies the input file to convert. At least one file containing Address Space definition compliant with UANodeSet schema must be specified. Many files may be entered at once.", MetaValue = "filePath")]
    public IEnumerable<string> Filenames { get; set; }

    [Option('e', "export", HelpText = "Specifies the output file path containing the ModelDesign XML document.", MetaValue = "filePath")]
    public string ModelDesignFileName { get; set; }

    [Option('s', "stylesheet", HelpText = "Name of the stylesheet document (XSLT - eXtensible Stylesheet Language Transformations). With XSLT you can transform an XML document into any text document.", MetaValue = "stylesheetName")]
    public string Stylesheet { get; set; }

    [Option('n', "namespace", HelpText = "Specifies the namespace for the generated types. If not specified last imported model is used for export.", MetaValue = "ns")]
    public string IMNamespace { get; set; }

    [Option("nologo", HelpText = "If present suppresses the banner.")]
    public bool NoLogo { get; set; }
  }
}