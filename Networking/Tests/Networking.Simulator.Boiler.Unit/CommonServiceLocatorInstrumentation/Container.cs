//___________________________________________________________________________________
//
//  Copyright (C) 2018, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UAOOI.Networking.Simulator.Boiler.UnitTest.CommonServiceLocatorInstrumentation
{
  internal class Container : ServiceLocatorImplBase
  {

    public Container(IEnumerable<object> list)
    {
      m_ObjectsContainer = list;
    }

    private readonly IEnumerable<object> m_ObjectsContainer;
    protected override object DoGetInstance(Type requestedType, string key)
    {
      return String.IsNullOrEmpty(key) ? m_ObjectsContainer.First(o => requestedType.IsAssignableFrom(o.GetType()))
                                       : m_ObjectsContainer.First(o => requestedType.IsAssignableFrom(o.GetType()) && Equals(key, o.GetType().FullName));
    }
    protected override IEnumerable<object> DoGetAllInstances(Type requestedType)
    {
      return m_ObjectsContainer.Where(o => requestedType.IsAssignableFrom(o.GetType()));
    }
  }
}
