//__________________________________________________________________________________________________
//
//  Copyright (C) 2022, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GitHub: https://github.com/mpostol/OPC-UA-OOI/discussions
//__________________________________________________________________________________________________

using System;
using System.Collections.Generic;
using UAOOI.SemanticData.InformationModelFactory;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IValidator - interface capturing functionality supporting the Ind=formation Model export and validation from the Address Space
  /// </summary>
  internal interface IValidator
  {
    /// <summary>
    /// Validates <paramref name="nodeContext" /> and exports it using an object of <see cref="IModelFactory" />  type.
    /// </summary>
    /// <param name="nodeContext">The node context to be validated and exported.</param>
    /// <param name="allNodesInConcern">list of selected members to export.</param>
    /// <param name="exportFactory">A model export factory.</param>
    /// <param name="parentReference">The reference to parent node.</param>
    /// <param name="validateExportNode2Model">It creates the node at the top level of the model. Called if the node has reference to another node that cannot be defined as a child.</param>
    void ValidateExportNode(IUANodeBase nodeContext, List<IUANodeBase> allNodesInConcern, INodeContainer exportFactory, Action<IUANodeContext> validateExportNode2Model, UAReferenceContext parentReference);

    /// <summary>
    /// Validates <paramref name="nodeContext" /> and exports it using an object of <see cref="IModelFactory" />  type.
    /// </summary>
    /// <param name="nodeContext">The node context to be validated and exported.</param>
    /// <param name="allNodesInConcern">list of selected members to export.</param>
    /// <param name="exportFactory">A model export factory.</param>
    /// <param name="validateExportNode2Model">It creates the node at the top level of the model. Called if the node has reference to another node that cannot be defined as a child.</param>
    void ValidateExportNode(IUANodeBase nodeContext, List<IUANodeBase> allNodesInConcern, INodeContainer exportFactory, Action<IUANodeContext> validateExportNode2Model);
  }
}