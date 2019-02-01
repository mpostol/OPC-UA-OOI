//___________________________________________________________________________________
//
//  Copyright (C) 2019, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData.UnitTest.Helpers;

namespace UAOOI.Networking.SemanticData.UnitTest.MessageHandlerFactory
{
  internal abstract class BinaryDataTransferGraphBaseFixture
  {
    #region IBinaryStreamObservable
    public IAssociationState State { get; set; } = new MyState();
    public void AttachToNetwork()
    {
      NumberOfAttachToNetwork++;
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
      DisposeCount++;
    }
    #endregion

    internal virtual void AssertConsistency()
    {
      Assert.AreEqual<int>(1, DisposeCount);
    }

    #region private
    internal int NumberOfAttachToNetwork = 0;
    internal int DisposeCount = 0;
    #endregion

  }
}
