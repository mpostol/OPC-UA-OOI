//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommandLine;
using System;
using System.Collections.Generic;

namespace UAOOI.SemanticData.AddressSpaceTestTool.CommandLineSyntax
{

  internal static class Extensions
  {

    public static void Parse<T>(this string[] args, Action<T> RunCommand, Action<IEnumerable<Error>> dump)
    {
      ParserResult<T> parserResult = Parser.Default
        .ParseArguments<T>(args)
        .WithParsed<T>((T opts) => { RunCommand(opts); })
        .WithNotParsed<T>(dump);
    }

  }
}
