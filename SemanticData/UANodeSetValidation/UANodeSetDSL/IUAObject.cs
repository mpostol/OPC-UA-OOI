﻿//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

namespace UAOOI.SemanticData.AddressSpace.Abstractions
{
  public interface IUAObject : IUAInstance
  {
    byte EventNotifier { get; set; }
  }
}