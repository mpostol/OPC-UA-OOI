//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.Networking.Simulator.Boiler.AddressSpace;
using CommsvrClassess = Commsvr.UA.Examples.BoilersSet;
using tempuriClasses = tempuri.org.UA.Examples.BoilerType;

namespace UAOOI.Networking.Simulator.Boiler.Model
{

  internal class BoilersSet : FolderState, ISemanticDataSource
  {

    internal static BoilersSet Factory { get { return m_Factory.Value; } }

    #region ISemanticDataSource
    /// <summary>
    /// Gets the semantic data set sources.
    /// </summary>
    /// <param name="registerSemanticData">The register semantic data.</param>
    public void GetSemanticDataSources(RegisterSemanticData registerSemanticData)
    {
      foreach (BaseInstanceState _state in m_Boilers)
      {
        SemanticDataSetSource _sd = new SemanticDataSetSource(_state);
        foreach (KeyValuePair<string, IVariable> item in _sd)
          registerSemanticData(_sd.SemanticDataSetRootBrowseName, item.Key, item.Value);
      }
    }
    #endregion

    #region private
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      foreach (BaseInstanceState _state in m_Boilers)
        _state.Dispose();
    }
    private static Lazy<BoilersSet> m_Factory = new Lazy<BoilersSet>(() => new BoilersSet());
    private List<BaseInstanceState> m_Boilers = new List<BaseInstanceState>();
    private BoilersSet() : base(null, CommsvrClassess.BrowseNames.BoilersArea)
    {
      m_Boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerAlpha));
      m_Boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerBravo));
      m_Boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerCharlie));
      m_Boilers.Add(new tempuriClasses.BoilerState(this, CommsvrClassess.BrowseNames.BoilerDelta));
    }
    #endregion

  }
}
