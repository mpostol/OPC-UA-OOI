
using System.Collections.Generic;
using CommsvrClassess = Commsvr.UA.Examples.BoilersSet;
using tempuriClasses = tempuri.org.UA.Examples.BoilerType;

namespace UAOOI.Networking.Simulator.Boiler.Model
{

  internal class BoilersSet: AddressSpace.FolderState
  {
    internal BoilersSet(): base(null, CommsvrClassess.BrowseNames.BoilersArea)
    {
      _boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerAlpha));
      _boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerBravo));
      _boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerCharlie));
      _boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerDelta));
    }

    private List<tempuriClasses.BoilerState> _boilers = new List<tempuriClasses.BoilerState>();

  }

}
