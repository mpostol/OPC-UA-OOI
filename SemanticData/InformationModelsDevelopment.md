# Information Models Development

## Methodology

OPC UA specification provides a standard Information Model domain, which contains a set of predefined types and instances. This domain may be extended by designing a new one. Even though the standard Information Model contains a rich set of predefined types, the type concept allows designers to freely define types according to the application needs. New types are derived from the existing ones. The derived types inherit all features of the base types but can include modifications to make the new types more appropriate for information to be represented. This new information model covered by the domain may be the subject of a companion specification or proprietary solution. In any case new definitions must be uniquely named and self-contained except for external type references. All not predefined types (not belonging to the standard domain) must be exposed in the Address Space by the server.

The model design engineering is an emerging discipline in which engineers develop new models with a primary emphasis on convergence between information describing the state and behavior of a selected real-time process and simplified representation of the process by process data. The data is formally described in terms of types. Generally speaking there are two approaches possible:

- Design a custom model that meets requirements of a proprietary process (Section [*`ReferenceApplication` Producer - Boilers Set Simulator*][BlrsStSmultr])
- Adopt an existing model released as a companion specification to meet the requirements of a proprietary process.

To improve performance of independent model developments as a result of reusability and, what is more important, to promote unification of results there are many activities aimed at designing models for selected processes. The unification enables vendors to commence independent developments of selected application aware products. In this section a case studies illustrating both approaches is presented.

## See also

- [`ReferenceApplication` Producer - Boilers Set Simulator][BlrsStSmultr]

[BlrsStSmultr]:../Networking/Simulator.Boiler/README.md
