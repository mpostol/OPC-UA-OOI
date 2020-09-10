//____________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/TP
//____________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Dynamic;

namespace UAOOI.Networking.DataRepository.AzureGateway
{
  /// <summary>
  /// Class RepositoryGroup - it is a process state replica.
  /// Implements the <see cref="DynamicObject" />
  /// </summary>
  /// <seealso cref="DynamicObject" />
  internal class RepositoryGroup : DynamicObject
  {
    #region DynamicObject

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      result = null;
      if (!_processReplica.ContainsKey(binder.Name))
        return false;
      result = GetPropertyValue(binder.Name);
      return true;
    }

    /// <summary>
    /// Provides the implementation for operations that invoke a member. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"></see> class can override this method to specify dynamic behavior for operations such as calling a method.
    /// </summary>
    /// <param name="binder">Provides information about the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"></see> class, binder.Name returns "SampleMethod". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
    /// <param name="args">The arguments that are passed to the object member during the invoke operation. For example, for the statement sampleObject.SampleMethod(100), where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject"></see> class, args[0] is equal to 100.</param>
    /// <param name="result">The result of the member invocation.</param>
    /// <returns>true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
    public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
    {
      result = null;
      return false;
    }

    #endregion DynamicObject

    #region API

    public Action<type> AddProperty<type>(string propertyName)
    {
      if (_processReplica.ContainsKey(propertyName))
        throw new ArgumentOutOfRangeException($"Duplicated property name: {propertyName}");
      _processReplica.Add(propertyName, default(type));
      object _value = _processReplica[propertyName];
      return x => Updater<type>(propertyName, x);
    }

    #endregion API

    #region private

    private readonly Dictionary<string, object> _processReplica = new Dictionary<string, object>();

    private object GetPropertyValue(string propertyName)
    {
      object result = null;
      if (_processReplica.ContainsKey(propertyName))
        result = _processReplica[propertyName];
      return result;
    }

    private void Updater<type>(string propertyName, type value)
    {
      _processReplica[propertyName] = value;
    }

    #endregion private
  }
}
