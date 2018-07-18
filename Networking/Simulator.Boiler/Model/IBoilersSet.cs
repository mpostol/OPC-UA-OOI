using System;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;

namespace UAOOI.Networking.Simulator.Boiler.Model
{
  internal interface IBoilersSet: IDisposable
  {
    void GetSemanticDataSetSources(Action<string, string, IVariable> registerSemanticData);
  }
}